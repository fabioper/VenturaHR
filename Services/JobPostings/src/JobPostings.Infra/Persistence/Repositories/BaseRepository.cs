using Common.Abstractions;
using JobPostings.Infra.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, IAggregateRoot
{
    private readonly DbContext _context;
    private readonly DbSet<T> _entity;

    public BaseRepository(JobPostingsContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll() => await _entity.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<T>> GetAllBy(ISpecification<T> specification)
        => await _entity.Specify(specification).ToListAsync();

    public async Task<T?> FindById(Guid id)
        => await _entity.FirstOrDefaultAsync(x => x.Id == id);

    public async Task Add(T entity)
    {
        await _entity.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Remove(T entity)
    {
        _entity.Remove(entity);
        await _context.SaveChangesAsync();
    }
}