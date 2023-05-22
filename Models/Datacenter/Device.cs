using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using infrastracture_api.Models.Domains;
using Microsoft.Build.Framework;

namespace infrastracture_api.Models.Datacenter;

public class Device
{
    [GraphQLType(typeof(IdType))]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string DeviceName { get; set; } = string.Empty;
    //public InfraDomain? Domain { get; set; }
    public DeviceType Type { get; set; } = DeviceType.StandaloneServer;
    public bool IsActive { get; set; } = true;
    public string? Manufacturer { get; set; } = string.Empty;
    public string? Model { get; set; } = string.Empty;
    
    public long? DatacenterId { get; set; }
    public Datacenter? Datacenter { get; set; }

    public enum DeviceType
    {
        StandaloneServer,
        Router,
        Switch,
        Hub,
        Hypervisor
    }
}