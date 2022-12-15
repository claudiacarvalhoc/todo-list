using ToDoList.Core.Converters;
using ToDoList.Core.Dtos;
using ToDoList.Core.Entities;

using FluentAssertions;
using ToDoList.Core.Exceptions;

namespace ToDoList.Test.Unit.Core;

public class ConverterTest
{
    [Test]
    public void ConvertToTodo_Should_Convert_TodoDto_To_Todo_Successfully()
    {
        var dto = new TodoDto
        {
            Id = "bf94096c-3831-4c0d-b539-fdfc5559b8ea",
            Name = "Clean code",
            Description = "Clean code",
            Status = "New",
        };

        var todo = dto.ConvertToTodo();

        todo.Should().NotBeNull();
        todo.Id.Should().Be("bf94096c-3831-4c0d-b539-fdfc5559b8ea");
        todo.Name.Should().Be("Clean code");
        todo.Description.Should().Be("Clean code");
        todo.Status.Should().Be(Status.New);
    }
    
    [Test]
    public void ConvertToTodo_When_Receiving_Not_Mapped_Enum_Should_Return_Exception()
    {
        var dto = new TodoDto
        {
            Id = "bf94096c-3831-4c0d-b539-fdfc5559b8ea",
            Name = "Clean code",
            Description = "Clean code",
            Status = "XPTO",
        };
        
        dto.Invoking(y => y.ConvertToTodo())
            .Should().Throw<NotPossibleConversionException>()
            .WithMessage("Status must be [New,InProgress,Done].");
    }
    
    [Test]
    public void ConvertToTodo_Should_Convert_CreationTodoDto_To_Todo_Successfully()
    {
        var dto = new CreationTodoDto
        {
            Name = "Clean code",
            Description = "Clean code",
        };

        var todo = dto.ConvertToTodo();

        todo.Should().NotBeNull();
        todo.Id.Should().NotBeNull();
        todo.Name.Should().Be("Clean code");
        todo.Description.Should().Be("Clean code");
        todo.Status.Should().Be(Status.New);
    }
    
    [Test]
    public void ConvertToTodoDto_Should_Convert_Todo_To_TodoDto_Successfully()
    {
        var dto = new Todo
        {
            Id = "bf94096c-3831-4c0d-b539-fdfc5559b8ea",
            Name = "Clean code",
            Description = "Clean code",
            Status = Status.New,
        };

        var todo = dto.ConvertToTodoDto();

        todo.Should().NotBeNull();
        todo.Id.Should().NotBeNull();
        todo.Name.Should().Be("Clean code");
        todo.Description.Should().Be("Clean code");
        todo.Status.Should().Be("New");
    }
}