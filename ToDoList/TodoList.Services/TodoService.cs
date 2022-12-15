using Microsoft.Extensions.Logging;
using ToDoList.Core.Converters;
using ToDoList.Core.Dtos;
using ToDoList.Core.Exceptions;
using TodoList.Repository;

namespace TodoList.Services;

public class TodoService : IService<TodoDto>
{
    private readonly ITodoRepository _todoRepository;
    private readonly ILogger<TodoService> _logger;
    
    public TodoService(ILogger<TodoService> logger, ITodoRepository repository)
    {
        _todoRepository = repository;
        _logger = logger;
    }
    
    public async Task<List<TodoDto>> GetAllAsync()
    {
        var todos = await _todoRepository.GetAsync();
        return todos.Select(todoItem => todoItem.ConvertToTodoDto()).ToList();
    }

    public async Task<TodoDto?> GetAsync(string id)
    {
        var todo = await _todoRepository.GetAsync(id);
        
        return todo?.ConvertToTodoDto() ?? null;
    }

    public async Task<TodoDto> AddAsync(TodoDto newItem)
    {
        var todoItemToBeCreated = newItem.ConvertToTodo();
        
        try
        {
            await _todoRepository.CreateAsync(todoItemToBeCreated);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Storing Data: {todoItemToBeCreated.ToString()}. Error: {e.Message} \n StackTrace: {e.StackTrace}");
            throw;
        }

        var insertedTodoItem = await _todoRepository.GetAsync(todoItemToBeCreated.Id);

        if (insertedTodoItem is not null) return insertedTodoItem.ConvertToTodoDto();
        
        _logger.LogError($"Cannot fetch stored data: {todoItemToBeCreated.ToString()}.");
        throw new ItemNotRetrievedException($"Cannot fetch stored data: {todoItemToBeCreated.ToString()}.");
    }

    public async Task<TodoDto> UpdateAsync(string id, TodoDto updatedItem)
    {
        if (updatedItem.Id != id)
        {
            throw new IdDoesntMatchObjectIdentifierException($"Cannot update item because Id: {id} doesn't match object identifier - Object: {updatedItem.ToString()}.");
        }
        
        var todoItemToBeUpdated = updatedItem.ConvertToTodo();
        
        try
        {
            await _todoRepository.UpdateAsync(id, todoItemToBeUpdated);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Update Data: {todoItemToBeUpdated.ToString()}. Error: {e.Message} \n StackTrace: {e.StackTrace}");
            throw;
        }

        var updatedTodoItem = await _todoRepository.GetAsync(id);

        if (updatedTodoItem is not null) return updatedTodoItem.ConvertToTodoDto();
        
        _logger.LogError($"Cannot fetch stored data: {todoItemToBeUpdated.ToString()}.");
        throw new ItemNotRetrievedException($"Cannot fetch stored data: {todoItemToBeUpdated.ToString()}.");
    }

    public Task RemoveAsync(string id)
    {
        return _todoRepository.RemoveAsync(id);
    }
}