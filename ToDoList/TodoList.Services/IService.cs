namespace TodoList.Services;

public interface IService<T>
{
    public Task<List<T>> GetAllAsync();
    public Task<T?> GetAsync(string id);
    public Task<T> AddAsync(T newItem);
    public Task<T> UpdateAsync(string id, T updatedItem);
    public Task RemoveAsync(string id);
}