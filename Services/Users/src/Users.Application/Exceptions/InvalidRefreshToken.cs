namespace Users.Application.Exceptions;

public class InvalidRefreshToken : Exception
{
    public InvalidRefreshToken() : base("Invalid Refresh Token") { }
}