namespace Users.Api.Common.Exceptions;

public class InvalidCredentialException : Exception
{
    public InvalidCredentialException() : base("Invalid credentials")
    {
    }
}