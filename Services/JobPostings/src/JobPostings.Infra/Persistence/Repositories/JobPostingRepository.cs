using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobPostingRepository : BaseRepository<JobPosting, JobPostingId>, IJobPostingRepository
{
    private readonly ModelContext _context;

    public JobPostingRepository(ModelContext context) : base(context) => _context = context;

    public async Task<List<JobPosting>> FindByCompanyOfId(CompanyId companyId)
    {
        return await _context.JobPostings
                             .Include(x => x.Company)
                             .Where(x => x.Company.Id == companyId)
                             .ToListAsync();
    }
}