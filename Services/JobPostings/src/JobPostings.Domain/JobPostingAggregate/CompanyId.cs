namespace JobPostings.Domain.JobPostingAggregate;

public record CompanyId
{
    public Guid Id { get; init; }

    public CompanyId(string id) => Id = Guid.Parse(id);

    public CompanyId(Guid id) => Id = id;
}