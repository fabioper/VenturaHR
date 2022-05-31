using JobPostings.Domain.Aggregates.Applications;
using JobPostings.Domain.Common;
using JobPostings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobApplicationRepository :
    BaseRepository<JobApplication, ApplicationId>,
    IJobApplicationRepository
{
    private readonly ModelContext _context;

    public JobApplicationRepository(ModelContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobApplication>> GetAllByJobCompanyOfId(JobPostingId jobPostingId)
    {
        return await _context.Applications
                             .Include(x => x.JobPosting)
                             .Where(x => x.JobPosting.Id == jobPostingId)
                             .ToListAsync();
    }
}