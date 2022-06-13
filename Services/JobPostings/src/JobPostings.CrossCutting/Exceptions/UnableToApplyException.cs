using JobPostings.CrossCutting.Resources;

namespace JobPostings.CrossCutting.Exceptions;

public class UnableToApplyException : Exception
{
    public UnableToApplyException() : base(ExceptionMessagesResources.UnableToApply)
    {
    }
}