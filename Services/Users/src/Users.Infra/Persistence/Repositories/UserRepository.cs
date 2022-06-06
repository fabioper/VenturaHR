using Microsoft.EntityFrameworkCore;
using Users.Domain.Models.Entities;
using Users.Domain.Models.ValueObjects;
using Users.Domain.Repositories;

namespace Users.Infra.Persistence.Repositories;

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

    public async Task<User?> FindById(Guid id)
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

    public async Task<User?> FindByEmail(string email)
        => await _entity.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User?> FindByRegistration(Registration registration)
        => await _entity.FirstOrDefaultAsync(x => x.Registration.Number == registration.Number);
}