namespace JobPostings.Domain.Exceptions;

public class JobPostingAlreadyAppliedException : Exception
{
    public JobPostingAlreadyAppliedException() : base("Applicant already applied to this JobPosting")
    {
    }
}