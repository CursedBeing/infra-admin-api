using infrastracture_api.Models.DbOps;
using Microsoft.EntityFrameworkCore;

namespace infrastracture_api.Models.Datacenter;

public class DeviceDbOps: IDbOps<Device>
{
    private ILogger<DeviceDbOps> _logger;
    private IDbContextFactory<AppDbContext> _factory;

    public DeviceDbOps(ILogger<DeviceDbOps> logger, IDbContextFactory<AppDbContext> factory)
    {
        _logger = logger;
        _factory = factory;
    }
    
    public async Task Create(Device entity)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            await ctx.Devices.AddAsync(entity);
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public async Task Update(Device entity)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            ctx.Devices.Update(entity);
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public async Task Delete(Device entity)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            ctx.Devices.Remove(entity);
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public List<Device> GetAll()
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            return ctx.Devices
                .AsNoTracking()
                .ToList();
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public Device? FindById(long id)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            return ctx.Devices
                .AsNoTracking()
                .FirstOrDefault(dc=>dc.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public Device? FindByName(string name)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            return ctx.Devices
                .AsNoTracking()
                .FirstOrDefault(dc=>dc.ServerName == name);
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
}