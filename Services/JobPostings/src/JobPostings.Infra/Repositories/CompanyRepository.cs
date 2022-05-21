using JobPostings.Domain.CompanyAggregate;
using JobPostings.Infra.Data;

namespace JobPostings.Infra.Repositories;

public class CompanyRepository : BaseRepository<Company, CompanyId>, ICompanyRepository
{
    private readonly ModelContext _context;

    public CompanyRepository(ModelContext context) : base(context) => _context = context;
}