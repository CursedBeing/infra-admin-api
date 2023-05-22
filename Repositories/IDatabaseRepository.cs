namespace infrastracture_api.Repositories;

public interface IDatabaseRepository<T>
{
    public List<T> GetAll();
    public Task<T> CreateAsync(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
}