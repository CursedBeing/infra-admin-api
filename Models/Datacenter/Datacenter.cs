using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infrastracture_api.Models.Datacenter;
/// <summary>
/// Класс описывает сущность ЦОД. Информация о цоде, контактах представителей ЦОДа и расположение.
/// </summary>
[Table("datacenters")]
[GraphQLDescription("Сущность Датацентр. Описывает как локальные так и внешние датацентры")]
public class Datacenter
{
    [GraphQLType(typeof(IdType))]
    public long Id { get; set; }
    [GraphQLDescription("Имя датацентра")]
    [Required,StringLength(250)] 
    public string Name { get; set; } = string.Empty;
    [GraphQLDescription("Физическое расположение оборудования")]
    public string? Location { get; set; } = string.Empty;
    [GraphQLDescription("Имя контактного лица датацентра")]
    public string? ContactName { get; set; }
    [GraphQLDescription("Email контактного лица датацентра")]
    public string? ContactEmail { get; set; }
    [GraphQLDescription("Телефон контактного лица датацентра")]
    public string? ContactPhone { get; set; }
    [GraphQLDescription("Сайт")]
    public string? ContactSite { get; set; }
    [GraphQLDescription("Флаг указывает на то что датацентр расположен в другой компании")]
    public bool? IsExternal { get; set; } = false;
    public bool? IsActive { get; set; } = true;

    public DateTime? Created { get; set; } = DateTime.Now;
    public DateTime? Updated { get; set; } = DateTime.Now;
    
    [GraphQLDescription("Сетевые устройства")]
    public List<NetworkDevice>? NetDevices { get; set; }
    [GraphQLDescription("Гипервизоры и хосты виртуализации")]
    public List<HvHostDevice>? Hypervisors { get; set; }
}