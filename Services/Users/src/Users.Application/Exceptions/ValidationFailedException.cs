namespace Users.Application.Exceptions;

public class ValidationFailedException : Exception
{
    public IDictionary<string,string> Errors { get; }

    public ValidationFailedException(IDictionary<string, string> rules) : base("Validation failed")
        => Errors = rules;
}