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
        return orderedQuery.Where(x => !filter.Applicant.HasValue || x.Applicant.Id == filter.Applicant.Value);
    }
}