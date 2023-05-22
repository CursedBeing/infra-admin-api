using infrastracture_api.Models.Datacenter;
using Microsoft.EntityFrameworkCore;

namespace infrastracture_api.Repositories;

public class NetworkDevicesDbRepository: IDatabaseRepository<NetworkDevice>
{
    private IDbContextFactory<AppDbContext> _factory;

    public NetworkDevicesDbRepository(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public List<NetworkDevice> GetAll()
    {
        using var ctx = _factory.CreateDbContext();
        return ctx.NetworkDevices
            .Include(nd => nd.Datacenter)
            .OrderBy(nd => nd.DeviceName)
            .AsNoTracking()
            .ToList();
    }

    public Task<NetworkDevice> CreateAsync(NetworkDevice entity)
    {
        throw new NotImplementedException();
    }

    public Task<NetworkDevice> UpdateAsync(NetworkDevice entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(NetworkDevice entity)
    {
        throw new NotImplementedException();
    }
}