using Users.CrossCutting.Resources;

namespace Users.CrossCutting.Exceptions;

public class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException() : base(ExceptionMessagesResources.InvalidRefreshToken) { }
}