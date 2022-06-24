using Users.CrossCutting.Resources;

namespace Users.CrossCutting.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string,string> Errors { get; }

    public ValidationException(IDictionary<string, string> errors) : base(ExceptionMessagesResources.Validation)
        => Errors = errors;
}