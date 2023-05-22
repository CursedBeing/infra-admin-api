using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using infrastracture_api.Models.Domains;

namespace infrastracture_api.Models.Virtualization;

[Table("vm")]
public class VirtualMachine
{
    [GraphQLType(typeof(IdType))] 
    public int Id { get; set; }

    [Required]
    [GraphQLDescription("Имя виртуальной машины без указания домена")]
    public string Name { get; set; } = string.Empty;
    [RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")]
    public string? IdAddress { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
    
    [AllowNull,GraphQLType(typeof(BooleanType))]
    public bool IsMonitored { get; set; } = true;
    
    [ForeignKey(nameof(Domain))] public int? DomainId { get; set; }
    public InfraDomain? Domain { get; set; }

    [ForeignKey(nameof(Host))] public long? HostId { get; set; }
    public HvHostDevice Host { get; set; }
}