namespace Users.Application.Exceptions;

public class InvalidCredentialException : Exception
{
    public InvalidCredentialException() : base("Invalid credentials")
    {
    }
}