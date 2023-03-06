using infrastracture_api.Models.PowerDNS;
using System.Text.Json;
using Host = infrastracture_api.Models.Host;

namespace infrastracture_api.Services;

public class PDNSService
{
    private ILogger<PDNSService> _logger;
    private IConfiguration _appConfig;
    public PDNSService(ILogger<PDNSService> logger, IConfiguration appConfig)
    {
        _logger = logger;
        _appConfig = appConfig;
    }
    public async Task CreateVmInPDNS(Host host)
    {
        try
        {
            //Создаем контент для новой записи.
            RecordList record = new() { Disabled = false, Content = host.IpAddress! };
            //Создаем рекордСет
            RrSet set = new()
            {
                Name = $"{host.HostName}.{host.Domain}.", 
                Ttl = 3600, 
                Type = "A", 
                ChangeType = "REPLACE", 
                Records = new RecordList[] { record }
            };
            //Создаем набор сетов для Json
            RrSets rrSets = new()
            {
                Sets = new[] { set }
            };
            await SendPatch("unix.teamstr.ru", rrSets);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
        
    }

    public async Task DeleteVmInPDNS(Host host)
    {
        try
        {
            //Создаем рекордСет
            RrSet set = new()
            {
                Name = $"{host.HostName}.{host.Domain}.", 
                Ttl = 3600, 
                Type = "A", 
                ChangeType = "DELETE"
            };
            //Создаем набор сетов для Json
            RrSets rrSets = new()
            {
                Sets = new[] { set }
            };
            await SendDelete("unix.teamstr.ru", rrSets);
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    private async Task SendPatch(string domain, RrSets sets)
    {
        try
        {
            _logger.LogInformation($"PDNS API Start on http://{_appConfig["pdns-host"]}/api/v1/servers/{_appConfig["pdns-server"]}/zones/{domain}.");
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Patch, $@"http://{_appConfig["pdns-host"]}/api/v1/servers/{_appConfig["pdns-server"]}/zones/{domain}.");
            request.Headers.Add("X-API-Key", $"{_appConfig["pdns-api-key"]}");
            var json = JsonSerializer.Serialize(sets);
            request.Content = new StringContent(json);
            _logger.LogInformation("PDNS REQUEST: {Request}", JsonSerializer.Serialize(request));
            _logger.LogInformation("PDNS REQUEST CONTENT: {Content}", json);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation("PDNS RESPONSE: {Request}", JsonSerializer.Serialize(response));
            _logger.LogInformation("PDNS REQUEST CONTENT: {Content}", await response.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            _logger.LogError("PDNS API Error: {Err}",e.Message);
            throw;
        }
    }
    
    private async Task SendDelete(string domain, RrSets sets)
    {
        try
        {
            _logger.LogInformation($"PDNS API Start on http://{_appConfig["pdns-host"]}/api/v1/servers/{_appConfig["pdns-server"]}/zones/{domain}.");
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Patch, $@"http://{_appConfig["pdns-host"]}/api/v1/servers/{_appConfig["pdns-server"]}/zones/{domain}.");
            request.Headers.Add("X-API-Key", $"{_appConfig["pdns-api-key"]}");
            var json = JsonSerializer.Serialize(sets);
            request.Content = new StringContent(json);
            _logger.LogInformation("PDNS REQUEST: {Request}", JsonSerializer.Serialize(request));
            _logger.LogInformation("PDNS REQUEST CONTENT: {Content}", json);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation("PDNS RESPONSE: {Response}", JsonSerializer.Serialize(response));
            _logger.LogInformation("PDNS REQUEST CONTENT: {Content}", await response.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            _logger.LogError("PDNS API Error: {Err}",e.Message);
            throw;
        }
    }
}