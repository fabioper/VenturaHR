using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Repositories;

namespace JobPostings.Infra.Persistence.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    private readonly JobPostingsContext _context;

    public CompanyRepository(JobPostingsContext context) : base(context) => _context = context;
}