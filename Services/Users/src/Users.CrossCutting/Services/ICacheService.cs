namespace Users.CrossCutting.Services;

public interface ICacheService
{
    Task Set(string key, string value);
    Task<string?> Get(string key);
    Task<T?> GetAs<T>(string anId);
    Task Set(string key, object value);
}