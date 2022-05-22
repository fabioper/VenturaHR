namespace Users.Api.Services.Contracts;

public interface IRedisClient
{
    Task Set(string key, string value);
    Task<string?> Get(string key);
}