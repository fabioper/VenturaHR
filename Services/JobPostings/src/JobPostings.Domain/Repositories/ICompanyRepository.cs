using Common.Abstractions;
using JobPostings.Domain.Aggregates.Companies;

namespace JobPostings.Domain.Repositories;

public interface ICompanyRepository : IBaseRepository<Company>
{
}