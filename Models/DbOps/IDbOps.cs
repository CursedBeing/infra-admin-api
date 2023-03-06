namespace infrastracture_api.Models.DbOps;

public interface IDbOps<in T>
{
    public Task Create(T entity);
    public Task Update(T entity);
    public Task Delete(T entity);
}