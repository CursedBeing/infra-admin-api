using System.ComponentModel.DataAnnotations.Schema;
using infrastracture_api.Models.Datacenter;

namespace infrastracture_api.Models.Domains;

/// <summary>
/// Описывает инфраструктурный домен, в котором разворачиваются вирт. машины
/// </summary>
[Table("infra_domains")]
public class InfraDomain: IDomain
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? Created { get; set; } = DateTime.Now;
    public DateTime? LastEdit { get; set; } = DateTime.Now;
}