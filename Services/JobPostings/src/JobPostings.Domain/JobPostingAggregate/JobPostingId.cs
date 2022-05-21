using Common.Abstractions;

namespace JobPostings.Domain.JobPostingAggregate;

public record JobPostingId : EntityId
{
    public Guid Id { get; private set; }

    public JobPostingId() => Id = Guid.NewGuid();

    public JobPostingId(string id) => Id = Guid.Parse(id);

    public JobPostingId(Guid id) => Id = id;
}