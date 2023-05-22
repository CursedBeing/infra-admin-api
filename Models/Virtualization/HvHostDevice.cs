using System.ComponentModel.DataAnnotations.Schema;
using infrastracture_api.Models.Datacenter;

namespace infrastracture_api.Models.Virtualization;

[Table("hypervisors")]
public class HvHostDevice : Device
{
    public string? OsType { get; set; }
    public string? OsVersion { get; set; }
    public string? MgmtIpAddress { get; set; }
    public string? IpAddress { get; set; }
}