using System.Text.Json.Serialization;

namespace infrastracture_api.Models.PowerDNS;

public class RrSets
{
    [JsonPropertyName("rrsets")]
    public RrSet[] Sets { get; set; }
}

public class RrSet
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("ttl")]
    public long Ttl { get; set; }
    [JsonPropertyName("changetype")]
    public string ChangeType { get; set; }
    [JsonPropertyName("records")]
    public RecordList[] Records { get; set; }
}

public class RecordList
{
    [JsonPropertyName("content")]
    public string Content { get; set; }
    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }
}