using System.ComponentModel.DataAnnotations.Schema;

namespace infrastracture_api.Models.Domains;
/// <summary>
/// Описывает сайты
/// </summary>
[Table("websites")]
public class Website: IDomain
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Created { get; set; } = DateTime.Now;
    public DateTime? LastEdit { get; set; } = DateTime.Now;
}