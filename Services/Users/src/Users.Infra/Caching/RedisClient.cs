using System.Text.Json;
using StackExchange.Redis;
using Users.Application.Contracts.Infrastructure;

namespace Users.Infra.Caching;

public class RedisClient : ICacheService
{
    private readonly IDatabase _redis;

    public RedisClient(IConnectionMultiplexer connection)
        => _redis = connection.GetDatabase();

    public async Task Set(string key, string value)
        => await _redis.StringSetAsync(key, value, TimeSpan.FromHours(2));

    public async Task<string?> Get(string key)
    {
        var value = await _redis.StringGetAsync(key);
        return value.HasValue ? value.ToString() : null;
    }

    public async Task<T?> GetAs<T>(string key)
    {
        var value = await _redis.StringGetAsync(key);
        return value.HasValue
            ? JsonSerializer.Deserialize<T>(value.ToString())
            : default;
    }

    public async Task Set(string key, object value)
        => await _redis.StringSetAsync(key, JsonSerializer.Serialize(value), TimeSpan.FromHours(2));
}