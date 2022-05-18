using JobPostings.Domain.JobPostingAggregate;
using JobPostings.Infra.Data;

namespace JobPostings.Infra.Repositories;

public class JobPostingRepository : BaseRepository<JobPosting>, IJobPostingRepository
{
    private readonly ModelContext _context;
    
    public JobPostingRepository(ModelContext context) : base(context)
    {
        _context = context;
    }
}