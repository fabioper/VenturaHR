namespace JobPostings.CrossCutting.Exceptions;

public class ExpiredJobPostingException : Exception
{
    public ExpiredJobPostingException() : base("Job Posting expired")
    {
    }
}