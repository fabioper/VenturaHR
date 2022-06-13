using Users.CrossCutting.Resources;

namespace Users.CrossCutting.Exceptions;

public class UnrecognizedUserTypeException : Exception
{
    public UnrecognizedUserTypeException(string userType) :
        base(string.Format(ExceptionMessagesResources.UnrecognizedUserType, userType)) { }
}