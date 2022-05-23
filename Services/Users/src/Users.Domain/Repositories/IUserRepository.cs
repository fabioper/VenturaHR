using Common.Abstractions;
using Users.Domain.Models.Entities;
using Users.Domain.Models.ValueObjects;

namespace Users.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User, UserId>
{
    Task<User?> FindByEmail(string email);
}