using System.ComponentModel.DataAnnotations.Schema;
using infrastracture_api.Models.Datacenter;
namespace infrastracture_api.Models.Virtualization;

[Table("vms")]
public class VirtualMachine: Datacenter.Host
{
    [ForeignKey(nameof(Host))]
    public long? HostId { get; set; }
    public Device? Host { get; set; }
    public bool AddedToMonintoring { get; set; } = true;
    
}