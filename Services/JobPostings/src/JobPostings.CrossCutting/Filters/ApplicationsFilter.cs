namespace JobPostings.CrossCutting.Filters;

public record ApplicationsFilter : BaseFilter
{
    public Guid? Applicant { get; set; }
}