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
    private readonly ITracer _tracer;

    public PromController(HostDbOps hostDb, ITracer tracer)
    {
        _hostDb = hostDb;
        _tracer = tracer;
    }

    [HttpGet("vm")]
    public IActionResult GetVmTargets()
    {
        var scope = _tracer.BuildSpan("GetVmHosts");
        List<string> VMs = new();
        var hosts = _hostDb.GetHostsFromDb();
        
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

        Target[] res = new[] { targets };

        return Ok(res);
    }
}