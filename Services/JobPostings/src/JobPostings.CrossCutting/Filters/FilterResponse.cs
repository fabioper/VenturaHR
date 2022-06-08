namespace JobPostings.CrossCutting.Filters;

public record FilterResponse<T>
{
    public int Page { get; init; }
    public int Total { get; init; }
    public List<T> Results { get; init; }
}