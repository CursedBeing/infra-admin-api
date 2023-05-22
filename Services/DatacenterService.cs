using infrastracture_api.Models.Datacenter;
using infrastracture_api.Repositories;
using Newtonsoft.Json;

namespace infrastracture_api.Services;

public class DatacenterService
{
    private ILogger<DatacenterService> _logger;
    private DatacenterDbRepository _rep;

    public DatacenterService(ILogger<DatacenterService> logger, DatacenterDbRepository rep)
    {
        _logger = logger;
        _rep = rep;
    }
    
    public List<Datacenter> GetDatacentersFromDb()
    {
        try
        {
            return _rep.GetAll();
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Trace}", e.Message, e.StackTrace);
            throw new Exception($"Не удалось получить список датацентров: {e.HResult} {e.Message}");
        }
    }
    public async Task<Datacenter> CreateNewDatacenter(Datacenter dc)
    {
        dc.Created = DateTime.Now;
        dc.Updated = dc.Created;
        if (dc.IsActive == null) dc.IsActive = true;
        if (dc.IsExternal == null) dc.IsExternal = true;
        
        try
        {
            dc = await _rep.CreateAsync(dc);
            return dc;
        }
        catch (Exception e)
        {
            _logger.LogError("{Msg}\n{Trace}", e.Message, e.StackTrace);
            throw new Exception($"Не удалось получить список датацентров: {e.HResult} {e.Message}");
        }
    }
    public async Task<Datacenter> UpdateDatacenter(Datacenter dc)
    {
        
        dc.Updated = DateTime.Now;
        _logger.LogInformation("Начинаем обновление датацентра");
        _logger.LogInformation("model: {Dc}", JsonConvert.SerializeObject(dc));
        try
        {
            var data = await _rep.UpdateAsync(dc);
            _logger.LogInformation("Датацентр {Name} {Id} был успешно обновлен", dc.Name,dc.Id);
            return data;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при обновлении датацентра в БД: {Msg}\n{Trace}", e.Message, e.StackTrace);
            //TODO Делать читаемую ошибку
            throw;
        }  
    }
    
}