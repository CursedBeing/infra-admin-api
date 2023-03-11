using infrastracture_api.Models.Datacenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace infrastracture_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : Controller
{
    private ILogger<DevicesController> _logger;
    private DeviceDbOps _db;


    public DevicesController(ILogger<DevicesController> logger, 
        DeviceDbOps db)
    {
        _logger = logger;
        _db = db;
    }
    
    [HttpGet]
    public IActionResult GetDevices()
    {
        try
        {
            return Ok(_db.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem("Internal server error", statusCode: 500);
        }
    }
    [HttpPost("add")]
    public async Task<IActionResult> NewDevice([FromBody] Device device)
    {
        try
        {
            if (device.DcId is null) return BadRequest();
            await _db.Create(device);
            return Ok(device);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem("Internal server error", statusCode: 500);
        }
    }
    [HttpPost("edit")]
    public async Task<IActionResult> EditDevice([FromBody] Device device)
    {
        try
        {
            await _db.Update(device);
            return Ok(device);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem("Internal server error", statusCode: 500);
        }
    }
    [HttpPost("Delete")]
    public async Task<IActionResult> DeleteDevice([FromBody] Device device)
    {
        try
        {
            await _db.Delete(device);
            return Ok(device);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            return Problem("Internal server error", statusCode: 500);
        }
    }
}