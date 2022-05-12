using Common;
using Microsoft.EntityFrameworkCore;

namespace Users.Api.Data.Repositories;

public class BaseRepository<T> : IRepository<T> where T : Entity, IAggregateRoot
{
    protected readonly DbContext Context;
    protected readonly DbSet<T> Entity;

    public BaseRepository(UsersContext context)
    {
        Context = context;
        Entity = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll() => await Entity.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<T>> GetAll(params ISpecification<T>[] specs)
    {
        IQueryable<T> query = Entity;

        foreach (var spec in specs)
        {
            query = spec.Includes
                        .Aggregate(query.AsQueryable(),
                            (current, include) => current.Include(include));

            query = query.Where(spec.Criteria);
        }

        return await query.ToListAsync();
    }

    public async Task<T?> FindById(long id)
    {
        return await Entity.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(T entity)
    {
        await Entity.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task Remove(T entity)
    {
        Entity.Remove(entity);
        await Context.SaveChangesAsync();
    }
}