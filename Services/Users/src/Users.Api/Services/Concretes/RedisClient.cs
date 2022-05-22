using StackExchange.Redis;
using Users.Api.Services.Contracts;

namespace Users.Api.Services.Concretes;

public class RedisClient : IRedisClient
{
    private readonly IDatabase _redis;

    public RedisClient(IConnectionMultiplexer connection)
        => _redis = connection.GetDatabase();

    public async Task Set(string key, string value)
        => await _redis.StringSetAsync(key, value, TimeSpan.FromHours(2));

    public async Task<string?> Get(string key)
    {
        var value = await _redis.StringGetAsync(key);
        if (value.HasValue)
            return value.ToString();
        return null;
    }
}