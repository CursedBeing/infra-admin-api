using infrastracture_api.Models.Datacenter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace infrastracture_api.Repositories;
/// <summary>
/// Репозиторий реализует работу с сущностью Datacenter
/// и базой данных постредством EFCore
/// </summary>
public class DatacenterDbRepository: IDatabaseRepository<Datacenter>
{
    private ILogger<DatacenterDbRepository> _logger;
    private IDbContextFactory<AppDbContext> _factory;

    public DatacenterDbRepository(ILogger<DatacenterDbRepository> logger, IDbContextFactory<AppDbContext> factory)
    {
        _logger = logger;
        _factory = factory;
    }

    /// <summary>
    /// Получает список датацентов со связанными устройствами
    /// </summary>
    public List<Datacenter> GetAll()
    {
        using var ctx = _factory.CreateDbContext();
        return ctx.Datacenters
            .Include(dc => dc.NetDevices)
            .Include(dc => dc.Hypervisors)
            .Where(dc=>dc.IsActive == true)
            .OrderBy(dc=>dc.Name)
            .AsNoTracking()
            .ToList();
    }

    /// <summary>
    /// Создает запись с датацентров в БД.
    /// Запрос выполняется без транзакций
    /// </summary>
    public async Task<Datacenter> CreateAsync(Datacenter entity)
    {
        //Зануляем идентификатор, мало ли что фронтендеры навставляли в модель
        entity.Id = 0;
        using var ctx = await _factory.CreateDbContextAsync();
        await ctx.Datacenters.AddAsync(entity);
        await ctx.SaveChangesAsync();
        return entity;
    }
    
    /// <summary>
    /// Обновляет запись в БД. Без транзакции
    /// </summary>
    public async Task<Datacenter> UpdateAsync(Datacenter entity)
    {
        using var ctx = await _factory.CreateDbContextAsync();
        ctx.Datacenters.Update(entity);
        await ctx.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет запись из БД. Без транзакции
    /// </summary>
    public async Task DeleteAsync(Datacenter entity)
    {
        using var ctx = await _factory.CreateDbContextAsync();
        ctx.Datacenters.Remove(entity);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(IEnumerable<Datacenter> entities)
    {
        using var ctx = await _factory.CreateDbContextAsync();
        ctx.Datacenters.RemoveRange(entities);
    }

    public Datacenter? FindDcByName(string name)
    {
        using var ctx = _factory.CreateDbContext();
        return ctx.Datacenters
            .AsNoTracking()
            .FirstOrDefault(dc => dc.Name == name);
    }
}