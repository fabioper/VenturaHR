using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.Companies;

public interface ICompanyRepository : IBaseRepository<Company, CompanyId>
{
}