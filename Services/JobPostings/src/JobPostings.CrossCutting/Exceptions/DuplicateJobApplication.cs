using JobPostings.CrossCutting.Resources;

namespace JobPostings.CrossCutting.Exceptions;

public class DuplicateJobApplication : Exception
{
    public DuplicateJobApplication() : base(ExceptionMessagesResources.DuplicateJobApplication)
    {
    }
}