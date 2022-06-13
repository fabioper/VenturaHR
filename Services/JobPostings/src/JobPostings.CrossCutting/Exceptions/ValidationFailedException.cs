using JobPostings.CrossCutting.Resources;

namespace JobPostings.CrossCutting.Exceptions;

public class ValidationFailedException : Exception
{
    public IDictionary<string,string> Errors { get; }

    public ValidationFailedException(IDictionary<string, string> rules)
        : base(ExceptionMessagesResources.ValidationFailed)
        => Errors = rules;
}