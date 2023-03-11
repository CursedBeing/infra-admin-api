using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace infrastracture_api.Models.Datacenter;

[Table("devices")]
public class Device: Host
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new long Id { get; set; }
    public DeviceType Type { get; set; } = DeviceType.StandaloneServer;
    public bool IsActive { get; set; } = true;
    public string? Manufacturer { get; set; } = string.Empty;
    public string? Model { get; set; } = string.Empty;
    public enum DeviceType
    {
        StandaloneServer,
        Router,
        Switch,
        Hub
    }
    
    [ForeignKey(nameof(Datacenter))]
    public long? DcId { get; set; }
    public Datacenter? Dc { get; set; }
    
}