using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobPostingRepository : BaseRepository<JobPosting>, IJobPostingRepository
{
    private readonly ModelContext _context;

    public JobPostingRepository(ModelContext context) : base(context) => _context = context;

    public async Task<List<JobPosting>> FindByCompanyOfId(Guid companyId)
    {
        return await _context.JobPostings
                             .Include(x => x.Company)
                             .Include(x => x.Criterias)
                             .Where(x => x.Company.Id == companyId)
                             .ToListAsync();
    }

    public new async Task<JobPosting?> FindById(Guid id)
    {
        return await _context.JobPostings
                             .Include(x => x.Company)
                             .Include(x => x.Criterias)
                             .FirstOrDefaultAsync(x => x.Id == id);
    }
}