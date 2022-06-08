using JobPostings.CrossCutting.Filters;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence.Repositories;

public class JobPostingRepository : BaseRepository<JobPosting>, IJobPostingRepository
{
    private readonly ModelContext _context;

    public JobPostingRepository(ModelContext context) : base(context) => _context = context;

    public async Task<IEnumerable<JobPosting>> GetAll(BaseFilter filter)
    {
        var jobPostings = _context.JobPostings.AsNoTracking()
            .Include(x => x.Company)
            .Include(x => x.Criterias);

        var paginatedQuery = jobPostings
            .Skip(filter.PageSize * (filter.Page - 1))
            .Take(filter.PageSize);

        var orderedQuery = paginatedQuery.OrderBy(x => x.CreatedAt);

        return await orderedQuery.ToListAsync();
    }

    public new async Task<JobPosting?> FindById(Guid id)
    {
        return await _context.JobPostings
                             .Include(x => x.Company)
                             .Include(x => x.Criterias)
                             .FirstOrDefaultAsync(x => x.Id == id);
    }
}