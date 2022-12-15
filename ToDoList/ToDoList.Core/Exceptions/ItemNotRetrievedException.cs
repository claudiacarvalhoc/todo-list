namespace ToDoList.Core.Exceptions;

public class ItemNotRetrievedException : Exception
{
    public ItemNotRetrievedException(string message) : base(message)
    {
           
    }
}