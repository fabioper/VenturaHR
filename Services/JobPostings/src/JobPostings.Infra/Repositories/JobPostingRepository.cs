using JobPostings.Domain.JobPostingAggregate;
using JobPostings.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Repositories;

public class JobPostingRepository : BaseRepository<JobPosting>, IJobPostingRepository
{
    private readonly ModelContext _context;
    
    public JobPostingRepository(ModelContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<JobPosting>> FindByCompanyOfId(string companyId)
    {
        return await _context.JobPostings.Include(x => x.Company)
                      .Where(x => x.Company.ExternalId == companyId)
                      .ToListAsync();
    }
}