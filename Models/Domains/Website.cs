using System.ComponentModel.DataAnnotations.Schema;

namespace infrastracture_api.Models.Domains;
/// <summary>
/// Описывает сайты
/// </summary>
[Table("websites")]
public class Website: IDomain
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}