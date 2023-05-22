using System.ComponentModel.DataAnnotations.Schema;

namespace infrastracture_api.Models.Datacenter;

[Table("netdevices")]
public class NetworkDevice: Device
{
    public int? PortCount { get; set; } = 4;
    public string? MgmtIpAddress { get; set; }
}