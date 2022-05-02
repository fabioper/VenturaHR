namespace Common.Exceptions;

public class EntityNotFoundException : ArgumentException
{
    public EntityNotFoundException(string entityName) : base($"{entityName} have not been found")
    {
    }
}