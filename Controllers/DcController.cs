using infrastracture_api.Models.Datacenter;
using Microsoft.AspNetCore.Mvc;

namespace infrastracture_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DcController: ControllerBase
{
    private ILogger<DcController> _logger;
    private DcDbOps _dc;

    public DcController(ILogger<DcController> logger, DcDbOps dc)
    {
        _logger = logger;
        _dc = dc;
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_dc.GetAll());
    }
    [HttpPost("add")]
    public async Task<IActionResult> AddDatacenter([FromBody] Datacenter dc)
    {
        try
        {
            await _dc.Create(dc);
            return Ok(dc);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem("Internal server error", statusCode:500);
        }
    }
    [HttpPost("edit")]
    public async Task<IActionResult> UpdateDatacenter([FromBody] Datacenter dc)
    {
        try
        {
            await _dc.Update(dc);
            return Ok(dc);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem("Internal server error", statusCode:500);
        }
    }
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteDatacenter([FromBody] long id)
    {
        try
        {
            var dc = _dc.FindById(id);
            if (dc is null) return BadRequest("Датацентр не существует");
            await _dc.Delete(dc);
            return Ok(dc);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem("Internal server error", statusCode:500);
        }
    }
}