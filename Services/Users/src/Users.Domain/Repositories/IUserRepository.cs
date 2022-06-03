using Common.Abstractions;
using Users.Domain.Models.Entities;

namespace Users.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmail(string email);
}