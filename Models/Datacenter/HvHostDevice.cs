using System.ComponentModel.DataAnnotations.Schema;

namespace infrastracture_api.Models.Datacenter;

[Table("hypervisors")]
public class HvHostDevice : Device
{
    public string? OsType { get; set; }
    public string? OsVersion { get; set; }
    public string? MgmtIpAddress { get; set; }
    public string? IpAddress { get; set; }
}