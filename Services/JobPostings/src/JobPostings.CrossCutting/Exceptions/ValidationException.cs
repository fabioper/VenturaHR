using JobPostings.CrossCutting.Resources;

namespace JobPostings.CrossCutting.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string,string> Errors { get; }

    public ValidationException(IDictionary<string, string> rules)
        : base(ExceptionMessagesResources.Validation)
        => Errors = rules;
}