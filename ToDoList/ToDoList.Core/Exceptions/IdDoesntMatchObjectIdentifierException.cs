namespace ToDoList.Core.Exceptions;

public class IdDoesntMatchObjectIdentifierException : Exception
{
    public IdDoesntMatchObjectIdentifierException(string message) : base(message)
    {
           
    }
}