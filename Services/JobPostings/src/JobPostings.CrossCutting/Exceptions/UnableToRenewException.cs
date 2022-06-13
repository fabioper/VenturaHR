namespace JobPostings.CrossCutting.Exceptions;

public class UnableToRenewException : Exception
{
    public UnableToRenewException() : base(
        "Unable to renew this job posting. This is probably because the job posting is already closed.")
    {
    }
}