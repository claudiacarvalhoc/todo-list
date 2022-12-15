using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Dtos;
using TodoList.Services;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly IService<TodoDto> _todoService;
    
    public TodoController(ILogger<TodoController> logger, IService<TodoDto> service)
    {
        _logger = logger;
        _todoService = service;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _todoService.GetAllAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail(string id)
    {
        return Ok(await _todoService.GetAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TodoDto todo)
    {
        var item = await _todoService.AddAsync(todo);
        return Ok(new ApiResponseDto<TodoDto> { IsSuccess = true, Data = item});
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] TodoDto todo)
    {
        var item = await _todoService.UpdateAsync(id, todo);
        return Ok(new ApiResponseDto<TodoDto> { IsSuccess = true, Data = item });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _todoService.RemoveAsync(id);
        return Ok(new ApiResponseDto<TodoDto> { IsSuccess = true });
    }
}