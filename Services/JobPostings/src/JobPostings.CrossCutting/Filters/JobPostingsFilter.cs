namespace JobPostings.CrossCutting.Filters;

public record JobPostingsFilter : BaseFilter
{
    public Guid? Company { get; init; }
    public string? Query { get; init; }
}