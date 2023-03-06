using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using infrastracture_api.Models;
using infrastracture_api.Models.DbOps;
using infrastracture_api.Services;
using Microsoft.AspNetCore.Mvc;
using Host = infrastracture_api.Models.Host;

namespace infrastracture_api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class HostsController: ControllerBase
{
    private ILogger<HostsController> _logger;
    private HostDbOps _db;
    private PDNSService _pdns;
        
    public HostsController(ILogger<HostsController> logger, 
        HostDbOps db, PDNSService pdns)
    {
        _logger = logger;
        _db = db;
        _pdns = pdns;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_db.GetHostsFromDb());
    }

    [HttpGet("{Id:long}")]
    public IActionResult GetSingle(long Id)
    {
        try
        {
            var data= _db.GetHostById(Id);
            if (data is null) return Ok(new Host());
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem(detail: "Internal server error", statusCode: 500);
        }
    }
    
    [HttpPost("Exist")]
    public IActionResult FindHost([FromBody] SearchHostModel model)
    {
        return Ok(HostIsExist(model.Search, model.Domain));
    }

    /// <summary>
    /// Добавляет в БД, ДНС и мониторинг новую виртуальную нашину
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("AddVm")]
    public async Task<IActionResult> AddVm([FromBody] Host model)
    {
        try
        {
            await _pdns.CreateVmInPDNS(model);
            await _db.Create(model);
            return Ok(model);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem(title: "Internal server error", detail: e.Message, statusCode: 500);
        }
    }
    
    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] Host model)
    {
        try
        {
            await _pdns.CreateVmInPDNS(model);
            await _db.Update(model);
            return Ok(model);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem(detail: "Internal server error", statusCode: 500);
        }
    }
    
    [HttpPost("Delete")]
    public async Task<IActionResult> Delete([FromBody] Host model)
    {
        try
        {
            if (string.IsNullOrEmpty(model.HostName)) 
                model = _db.GetHostById(model.Id) ?? throw new InvalidOperationException("Не найден такой");
            if (model is not null)
            {
                await _pdns.DeleteVmInPDNS(model);
                await _db.Delete(model);
            }
            return Ok(model);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem(detail: "Internal server error", statusCode: 500);
        }
    }

    private bool HostIsExist(string search, string? domain)
    {
        if (string.IsNullOrEmpty(domain))
        {
            Regex iprp = new Regex(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$");
            var res = iprp.Match(search);
            if (res.Success)
            {
                var host1 = _db.FindHostByIp(search);
                if (host1 is not null) return true;
            }
        }
        else
        {
            var host2 = _db.FindHostByName(search, domain);
            if (host2 is not null) return true;
        }
        return false;
    }
}