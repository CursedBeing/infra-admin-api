using System.ComponentModel.DataAnnotations;

namespace infrastracture_api.Models.Datacenter;

public class Host
{
    public long Id { get; set; }
    public string ServerName { get; set; } = string.Empty;
    [RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)(\.(?!$)|$)){4}$", ErrorMessage = "не является IP адресом")]
    public string? IpAddress { get; set; }
    [RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)(\.(?!$)|$)){4}$", ErrorMessage = "не является IP адресом")]
    public string? MgmtIp { get; set; }
    public string? Description { get; set; }
}