using Common.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence.Repositories;

public class BaseRepository<T, TId> : IBaseRepository<T, TId>
    where T : BaseEntity<TId>, IAggregateRoot where TId : EntityId
{
    private readonly DbContext _context;
    private readonly DbSet<T> _entity;

    public BaseRepository(ModelContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll() => await _entity.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<T>> GetAll(params ISpecification<T>[] specs)
    {
        IQueryable<T> query = _entity;

        foreach (var spec in specs)
        {
            query = spec.Includes
                        .Aggregate(query.AsQueryable(),
                            (current, include) => current.Include(include));

            query = query.Where(spec.Criteria);
        }

        return await query.ToListAsync();
    }
    
    public async Task<T?> FindById(TId id)
    {
        return await _entity.FirstOrDefaultAsync(x => x.Id == id);
    }

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