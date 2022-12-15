using ToDoList.Core.Entities;

namespace ToDoList.Core.Dtos;

public class TodoDto
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Status { get; set; }
    
    public override string ToString()
    {
        return $"[Id: {this.Id}, Name: {this.Name}, Description: {this.Description}, Status: {this.Status.ToString()}]";
    }
}