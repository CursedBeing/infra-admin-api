using infrastracture_api.Models.DbOps;
using infrastracture_api.Models.Prometheus;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace infrastracture_api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PromController:ControllerBase
{
    private HostDbOps _hostDb;

    public PromController(HostDbOps hostDb)
    {
        _hostDb = hostDb;
    }

    [HttpGet("vm")]
    public async Task<IActionResult> GetVmTargets()
    {
        List<string> VMs = new();
        var hosts = await _hostDb.GetForMonitoring();
        
        foreach (var host in hosts)
        {
            if (string.IsNullOrEmpty(host.HostName) || string.IsNullOrEmpty(host.Domain))
            {
                VMs.Add($"{host.IpAddress}:9273");
            }
            else
            {
                VMs.Add($"{host.HostName}.{host.Domain}:9273");
            }
        }

        IDictionary<string, string> labels = new Dictionary<string, string>();
        labels.Add("type", "vm");
        labels.Add("location", "yar");
        labels.Add("owner", "teamstr");
        var targets = new TargetWithLabels()
        {
            Targets = VMs.ToArray(),
            Labels = labels
        };

        Target[] res = new Target[] { targets };

        return Ok(res);
    }
}