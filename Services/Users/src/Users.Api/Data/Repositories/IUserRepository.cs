using Common.Abstractions;
using Users.Api.Models.Entities;
using Users.Api.Models.ValueObjects;

namespace Users.Api.Data.Repositories;

public interface IUserRepository<T> : IBaseRepository<T, UserId> where T : BaseUser
{
}