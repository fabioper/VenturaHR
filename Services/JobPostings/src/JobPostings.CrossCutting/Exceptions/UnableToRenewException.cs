using JobPostings.CrossCutting.Resources;

namespace JobPostings.CrossCutting.Exceptions;

public class UnableToRenewException : Exception
{
    public UnableToRenewException() : base(ExceptionMessagesResources.UnableToRenew)
    {
    }
}