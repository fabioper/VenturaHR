using Common.Abstractions;
using Users.Api.Models.Entities;

namespace Users.Api.Data.Repositories;

public interface IUserRepository<T> : IRepository<T> where T : BaseUser
{
    Task<T?> FindByExternalId(string externalId);
}