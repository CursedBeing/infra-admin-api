using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace infrastracture_api.Models;

public class Host
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string HostName { get; set; } = string.Empty;
    public string? Domain { get; set; } = "unix.teamstr.ru";
    public string? IpAddress { get; set; } = "192.168.1.1";
    
    //Prometheus
    public bool MonitorEnabled { get; set; } = true;
    public string? TelegrafPort { get; set; } = "9273";
    public string? Description { get; set; } = string.Empty;
    
    [ForeignKey(nameof(Type))]
    public long? TypeId { get; set; }
    public HostType? Type { get; set; }
}