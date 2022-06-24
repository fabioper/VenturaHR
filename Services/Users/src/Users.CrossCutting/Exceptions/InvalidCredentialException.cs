using Users.CrossCutting.Resources;

namespace Users.CrossCutting.Exceptions;

public class InvalidCredentialException : Exception
{
    public InvalidCredentialException() : base(ExceptionMessagesResources.InvalidCredentials)
    {
    }
}