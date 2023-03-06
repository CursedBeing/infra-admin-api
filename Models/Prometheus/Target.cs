namespace infrastracture_api.Models.Prometheus;

public class Target
{
    public string[] Targets { get; set; }
}

public class TargetWithLabels : Target
{
    public IDictionary<string,string> Labels { get; set; }
}