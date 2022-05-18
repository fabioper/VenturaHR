using JobPostings.Domain.CompanyAggregate;
using JobPostings.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    private readonly ModelContext _context;

    public CompanyRepository(ModelContext context) : base(context) => _context = context;

    public async Task<Company?> FindByExternalId(string externalId) =>
        await _context.Companies.FirstOrDefaultAsync(x => x.ExternalId == externalId);
}