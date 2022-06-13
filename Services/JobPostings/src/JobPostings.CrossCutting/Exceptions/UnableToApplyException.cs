namespace JobPostings.CrossCutting.Exceptions;

public class UnableToApplyException : Exception
{
    public UnableToApplyException() : base(
        "Unable to apply to this job posting. This job posting is probably expired or closed.")
    {
    }
}