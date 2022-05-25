using Common.Abstractions;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Repositories;

public interface ICompanyRepository : IBaseRepository<Company, CompanyId>
{
}