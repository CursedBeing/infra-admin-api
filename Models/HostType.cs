namespace infrastracture_api.Models;

public class HostType
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    public List<Host>? Hosts { get; set; }
}