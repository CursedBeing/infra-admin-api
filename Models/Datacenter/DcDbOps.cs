using infrastracture_api.Controllers;
using infrastracture_api.Models.DbOps;
using Microsoft.EntityFrameworkCore;

namespace infrastracture_api.Models.Datacenter;

public class DcDbOps: IDbOps<Datacenter>
{
    private IDbContextFactory<AppDbContext> _factory;
    private ILogger<DcDbOps> _logger;
    public DcDbOps(IDbContextFactory<AppDbContext> factory, 
        ILogger<DcDbOps> logger)
    {
        _factory = factory;
        _logger = logger;
    }
    public async Task Create(Datacenter entity)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            await ctx.Datacenters.AddAsync(entity);
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public async Task Update(Datacenter entity)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            ctx.Datacenters.Update(entity);
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public async Task Delete(Datacenter entity)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            ctx.Datacenters.Remove(entity);
            await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public List<Datacenter> GetAll()
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            return ctx.Datacenters
                .AsNoTracking()
                .Include(dc => dc.Devices)
                .ToList();
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public Datacenter? FindById(long id)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            return ctx.Datacenters
                .AsNoTracking()
                .FirstOrDefault(dc=>dc.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }
    public Datacenter? FindByName(string name)
    {
        using var ctx = _factory.CreateDbContext();
        try
        {
            return ctx.Datacenters
                .AsNoTracking()
                .FirstOrDefault(dc=>dc.Name == name);
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}\n{Stack}", e.Message, e.StackTrace);
            throw;
        }
    }

}