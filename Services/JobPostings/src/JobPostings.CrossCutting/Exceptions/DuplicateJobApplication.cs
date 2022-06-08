namespace JobPostings.CrossCutting.Exceptions;

public class DuplicateJobApplication : Exception
{
    public DuplicateJobApplication() : base("Applicant already applied to this JobPosting")
    {
    }
}