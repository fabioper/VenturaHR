using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.JobPostings;

public record JobPostingId : EntityId
{
    public Guid Id { get; private set; }

    public JobPostingId() => Id = Guid.NewGuid();

    public JobPostingId(string id) => Id = Guid.Parse(id);

    public JobPostingId(Guid id) => Id = id;
}