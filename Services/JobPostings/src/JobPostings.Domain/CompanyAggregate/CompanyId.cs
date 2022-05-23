using Common.Abstractions;

namespace JobPostings.Domain.CompanyAggregate;

public record CompanyId(Guid Id) : EntityId
{
    public CompanyId(string id) : this(Guid.Parse(id)) { }
}