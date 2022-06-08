using JobPostings.CrossCutting.Extensions;
using JobPostings.CrossCutting.Filters;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobPostingRepository : BaseRepository<JobPosting>, IJobPostingRepository
{
    private readonly ModelContext _context;

    public JobPostingRepository(ModelContext context) : base(context) => _context = context;

    public async Task<IEnumerable<JobPosting>> GetAll(JobPostingsFilter filter)
    {
        var jobPostings = _context.JobPostings.AsNoTracking()
            .Include(x => x.Company)
            .Include(x => x.Criterias);

        var orderedQueryable = jobPostings.OrderBy(x => x.CreatedAt);
        
        var filteredQuery = FilterQuery(filter, orderedQueryable);

        var paginatedQuery = filteredQuery.Paginate(filter);

        return await paginatedQuery.ToListAsync();
    }

    public new async Task<JobPosting?> FindById(Guid id)
    {
        return await _context.JobPostings
                             .Include(x => x.Company)
                             .Include(x => x.Criterias)
                             .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Count(JobPostingsFilter filter)
    {
        var jobPostings = _context.JobPostings;
        var filteredQuery = FilterQuery(filter, jobPostings);
        return await filteredQuery.CountAsync();
    }

    private static IQueryable<JobPosting> FilterQuery(JobPostingsFilter filter, IQueryable<JobPosting> jobPostings)
        => jobPostings.Where(x => !filter.Company.HasValue || x.Company.Id == filter.Company);
}