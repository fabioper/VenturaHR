using Users.Api.DTOs.Responses;

namespace Users.Api.Services.Contracts;

public interface IRedisClient
{
    Task Set(string key, string value);
    Task<string?> Get(string key);
    Task<T?> GetAs<T>(string anId);
    Task Set(string key, object value);
}