using ToDoList.Core.Entities;

namespace TodoList.Repository;

public interface ITodoRepository
{
    public Task<List<Todo>> GetAsync();
    public Task<Todo?> GetAsync(string id);
    public Task CreateAsync(Todo newBook);
    public Task UpdateAsync(string id, Todo updatedTodo);
    public Task RemoveAsync(string id);
}