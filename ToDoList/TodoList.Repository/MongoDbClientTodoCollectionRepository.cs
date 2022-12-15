using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Core.Entities;
using ToDoList.Core.Settings;

namespace TodoList.Repository;

public class MongoDbClientTodoCollectionRepository : ITodoRepository
{
    private readonly IMongoCollection<Todo> _todoCollection;

    public MongoDbClientTodoCollectionRepository(
        IOptions<TodoStoreDatabaseSettings> todoStoreDatabaseSettings)
    {
        // TODO: Use Dependency Injection to configure MongoClient
        var mongoClient = new MongoClient(
            todoStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            todoStoreDatabaseSettings.Value.DatabaseName);

        _todoCollection = mongoDatabase.GetCollection<Todo>(
            todoStoreDatabaseSettings.Value.BooksCollectionName);
    }
    
    
    public async Task<List<Todo>> GetAsync() => await _todoCollection.Find(_ => true).ToListAsync();

    public async Task<Todo?> GetAsync(string id) => await _todoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Todo newTodo) => await _todoCollection.InsertOneAsync(newTodo);

    public async Task UpdateAsync(string id, Todo updatedTodo) => await _todoCollection.ReplaceOneAsync(x => x.Id == id, updatedTodo);

    public async Task RemoveAsync(string id) => await _todoCollection.DeleteOneAsync(x => x.Id == id);
}