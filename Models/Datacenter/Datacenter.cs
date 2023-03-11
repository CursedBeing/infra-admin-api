using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infrastracture_api.Models.Datacenter;
/// <summary>
/// Класс описывает сущность ЦОД. Информация о цоде, контактах представителей ЦОДа и расположение.
/// </summary>
[Table("datacenters")]
public class Datacenter
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] 
    [StringLength(250)] 
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Физическое расположение оборудования или виртуальной машины.
    /// </summary>
    public string? Location { get; set; } = string.Empty;
    //Контактные данные представителей ЦОД
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactSite { get; set; }
    public List<Device>? Devices { get; set; }
}