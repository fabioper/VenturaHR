namespace JobPostings.CrossCutting.Filters;

public record JobPostingsFilter : BaseFilter
{
    public Guid? Company { get; set; }
}