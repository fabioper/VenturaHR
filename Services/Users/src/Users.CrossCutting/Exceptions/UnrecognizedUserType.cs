namespace Users.Api.Common.Exceptions;

public class UnrecognizedUserType : Exception
{
    public UnrecognizedUserType(string userType) :
        base($"Unrecognized user type: {userType}") { }
}