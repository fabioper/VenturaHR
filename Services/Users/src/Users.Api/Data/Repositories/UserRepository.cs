using Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Users.Api.Models.Entities;
using Users.Api.Models.ValueObjects;

namespace Users.Api.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbContext _context;
    private readonly DbSet<User> _entity;

    public UserRepository(UsersContext context)
    {
        _context = context;
        _entity = context.Set<User>();
    }

    public async Task<IEnumerable<User>> GetAll() => await _entity.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<User>> GetAll(params ISpecification<User>[] specs)
    {
        IQueryable<User> query = _entity;

        foreach (var spec in specs)
        {
            query = spec.Includes
                        .Aggregate(query.AsQueryable(),
                            (current, include) => current.Include(include));

            query = query.Where(spec.Criteria);
        }

        return await query.ToListAsync();
    }

    public async Task<User?> FindById(UserId id)
    {
        return await _entity.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(User entity)
    {
        await _entity.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Remove(User entity)
    {
        _entity.Remove(entity);
        await _context.SaveChangesAsync();
    }
}