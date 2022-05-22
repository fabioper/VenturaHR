namespace Users.Api.Common.Exceptions;

public class InvalidRefreshToken : Exception
{
    public InvalidRefreshToken() : base("Invalid Refresh Token") { }
}