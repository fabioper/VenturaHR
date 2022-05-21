using Common.Abstractions;

namespace JobPostings.Domain.CompanyAggregate;

public interface ICompanyRepository : IBaseRepository<Company, CompanyId>
{
}