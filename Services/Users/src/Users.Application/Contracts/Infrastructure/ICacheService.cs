namespace Users.Application.Contracts.Infrastructure;

public interface ICacheService
{
    Task Set(string key, string value);
    Task<string?> Get(string key);
    Task<T?> GetAs<T>(string key);
    Task Set(string key, object value);
}