using Common.Abstractions;

namespace JobPostings.Domain.CompanyAggregate;

public interface ICompanyRepository : IRepository<Company>
{
    Task<Company?> FindByExternalId(string externalId);
}