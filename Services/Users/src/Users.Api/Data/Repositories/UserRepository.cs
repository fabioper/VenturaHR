using Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Users.Api.Models.Entities;
using Users.Api.Models.ValueObjects;

namespace Users.Api.Data.Repositories;

public class UserRepository<T> : IUserRepository<T> where T : BaseUser
{
    private readonly DbContext _context;
    private readonly DbSet<T> _entity;

    public UserRepository(UsersContext context)
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

    public async Task<T?> FindById(UserId id)
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