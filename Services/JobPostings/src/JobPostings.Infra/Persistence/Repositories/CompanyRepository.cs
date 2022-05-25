using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Common;
using JobPostings.Domain.Repositories;

namespace JobPostings.Infra.Persistence.Repositories;

public class CompanyRepository : BaseRepository<Company, CompanyId>, ICompanyRepository
{
    private readonly ModelContext _context;

    public CompanyRepository(ModelContext context) : base(context) => _context = context;
}