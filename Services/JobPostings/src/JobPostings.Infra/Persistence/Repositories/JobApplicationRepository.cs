using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobApplicationRepository :
    BaseRepository<JobApplication>,
    IJobApplicationRepository
{
    private readonly ModelContext _context;

    public JobApplicationRepository(ModelContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobApplication>> GetAllByJobCompanyOfId(
        Guid companyId,
        Guid jobPostingId)
    {
        return await _context.Applications
                             .Include(x => x.JobPosting)
                             .ThenInclude(x => x.Company)
                             .Where(x => x.JobPosting.Company.Id == companyId)
                             .Where(x => x.JobPosting.Id == jobPostingId)
                             .ToListAsync();
    }

    public async Task<IEnumerable<JobApplication>> GetAllByApplicant(Guid applicantId)
    {
        return await _context.Applications
                             .Include(x => x.Applicant)
                             .Include(x => x.CriteriasAnswers)
                             .ThenInclude(x => x.Criteria)
                             .Include(x => x.JobPosting)
                             .ThenInclude(x => x.Company)
                             .Where(x => x.Applicant.Id == applicantId)
                             .ToListAsync();
    }
}