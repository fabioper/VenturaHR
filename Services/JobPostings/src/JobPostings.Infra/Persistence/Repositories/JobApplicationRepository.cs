using JobPostings.CrossCutting.Extensions;
using JobPostings.CrossCutting.Filters;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobApplicationRepository :
    BaseRepository<JobApplication>,
    IJobApplicationRepository
{
    private readonly JobPostingsContext _context;

    public JobApplicationRepository(JobPostingsContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobApplication>> GetJobPostingApplications(
        Guid companyId,
        Guid jobPostingId)
    {
        var applications = _context.Applications
            .Include(x => x.Applicant)
            .Include(x => x.Answers)
            .ThenInclude(x => x.Criteria);

        var filteredApplications = applications
            .Where(x => x.JobPosting.Company.Id == companyId)
            .Where(x => x.JobPosting.Id == jobPostingId);

        var applicationsOrderedByAverage = filteredApplications.OrderByDescending(x => x.Average);

        return await applicationsOrderedByAverage.ToListAsync();
    }

    public async Task<IEnumerable<JobApplication>> GetAll(ApplicationsFilter filter)
    {
        var applications = _context.Applications
            .Include(x => x.Applicant)
            .Include(x => x.Answers)
                .ThenInclude(x => x.Criteria)
            .Include(x => x.JobPosting)
                .ThenInclude(x => x.Company);

        var orderedQuery = applications.OrderBy(x => x.CreatedAt);
        
        var filteredQuery = FilterQuery(filter, orderedQuery);
       
        var paginatedQuery = filteredQuery.Paginate(filter);

        return await paginatedQuery.ToListAsync();
    }

    public async Task<int> Count(ApplicationsFilter filter)
    {
        var applications = _context.Applications.AsNoTracking();
        var filtered = FilterQuery(filter, applications);
        return await filtered.CountAsync();
    }

    private static IQueryable<JobApplication> FilterQuery(
        ApplicationsFilter filter, IQueryable<JobApplication> orderedQuery)
    {
        if (filter.Applicant.HasValue)
            orderedQuery = orderedQuery.Where(x => x.Applicant.Id == filter.Applicant.Value);

        if (filter.JobPosting.HasValue)
            orderedQuery = orderedQuery.Where(x => x.JobPosting.Id == filter.JobPosting.Value);

        return orderedQuery;
    }
}