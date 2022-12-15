using ToDoList.Core.Dtos;
using ToDoList.Core.Entities;
using ToDoList.Core.Exceptions;

namespace ToDoList.Core.Converters;

public static class TodoConverter
{
    public static Todo ConvertToTodo(this TodoDto dto)
    {
        return new Todo
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Status = ToEnum(dto.Status, Status.New),
        };
    }
    
    public static TodoDto ConvertToTodoDto(this Todo dto)
    {
        return new TodoDto
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Status = dto.Status.ToString(),
        };
    }

    public static Todo ConvertToTodo(this CreationTodoDto dto)
    {
        return new Todo
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Description = dto.Description,
            Status = Status.New,
        };
    }
    
    private static T ToEnum<T>(this string value, T defaultValue) where T : struct
    {
        if (string.IsNullOrEmpty(value))
        {
            return defaultValue;
        }

        return Enum.TryParse<T>(value, true, out var result) ? result : throw new NotPossibleConversionException($"Status must be {GetEnumValues<Status>()}.");
    }

    private static string GetEnumValues<T>()
    {
        var enumValues = Enum.GetNames((typeof(T)));
        return $"[{string.Join(",", enumValues)}]";
    }
}