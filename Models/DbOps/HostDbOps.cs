using Microsoft.EntityFrameworkCore;

namespace infrastracture_api.Models.DbOps;

public class HostDbOps: IDbOps<Host>
{
    private ILogger<HostDbOps> _logger;
    private IDbContextFactory<AppDbContext> _factory;

    public HostDbOps(ILogger<HostDbOps> logger, 
        IDbContextFactory<AppDbContext> factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public List<Host> GetHostsFromDb()
    {
        using var ctx = _factory.CreateDbContext();
        return ctx.Hosts
            .AsNoTracking()
            .Include(h=>h.Type)
            .ToList();
    }
    public Host? GetHostById(long id)
    {
        using var ctx = _factory.CreateDbContext();
        return ctx.Hosts
            .AsNoTracking()
            .Include(h=>h.Type)
            .FirstOrDefault(h => h.Id == id);
    }
    public Host? FindHostByName(string name, string domain)
    {
        using var ctx = _factory.CreateDbContext();
        var data = ctx.Hosts
            .AsNoTracking()
            .Where(h=>h.Domain == domain)
            .FirstOrDefault(h => h.HostName == name);
        return data;
    }
    public Host? FindHostByIp(string ip)
    {
        using var ctx = _factory.CreateDbContext();
        var data = ctx.Hosts
            .AsNoTracking()
            .FirstOrDefault(h => h.IpAddress == ip);
        return data;
    }
    public async Task Create(Host entity)
    {
        using var ctx = await _factory.CreateDbContextAsync();
        await ctx.Hosts.AddAsync(entity);
        await ctx.SaveChangesAsync();
    }
    public async Task Update(Host entity)
    {
        using var ctx = await _factory.CreateDbContextAsync();
        ctx.Hosts.Update(entity);
        await ctx.SaveChangesAsync();
    }
    public async Task Delete(Host entity)
    {
        using var ctx = await _factory.CreateDbContextAsync();
        ctx.Hosts.Remove(entity);
        await ctx.SaveChangesAsync();;
    }
    public async Task UpdateMass(List<Host> entities)
    {
        using var ctx = await _factory.CreateDbContextAsync();
        ctx.Hosts.UpdateRange(entities.ToArray());
        await ctx.SaveChangesAsync();
    }
    public async Task DeleteMass(List<Host> entities)
    {
        using var ctx = await _factory.CreateDbContextAsync();
        ctx.Hosts.RemoveRange(entities.ToArray());
        await ctx.SaveChangesAsync();
    }
}