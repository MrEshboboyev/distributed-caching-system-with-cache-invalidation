using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using StackExchange.Redis;
using System.Text.Json;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Security;

public sealed class RedisCacheRepository(
    IConnectionMultiplexer redis,
    IOptions<RedisSettings> settings
) : ICacheRepository
{
    private readonly IDatabase _redisDb = redis.GetDatabase();
    private readonly string _invalidationChannel = settings.Value.InvalidationChannel;

    public async Task<Result<CachedItem>> GetAsync(
        CacheKey key, 
        CancellationToken cancellationToken)
    {
        var redisValue = await _redisDb.StringGetAsync(key.Value);
        if (redisValue.IsNullOrEmpty)
            return Result.Failure<CachedItem>(
                DomainErrors.Cache.CacheMiss);

        return CachedItem.Create(
            Guid.NewGuid(),
            key.Value,
            JsonSerializer.Deserialize<byte[]>(redisValue!)!,
            CacheExpiration.Default // Replace with actual expiration from Redis
        );
    }

    public async Task<Result> SetAsync(
        CachedItem item, 
        CancellationToken cancellationToken)
    {
        var serializedValue = JsonSerializer.Serialize(item.Value);
        await _redisDb.StringSetAsync(
            item.Key,
            serializedValue,
            item.Expiration.AbsoluteExpiration
        );
        return Result.Success();
    }

    public async Task<Result> InvalidateAsync(
        CacheKey key, 
        CancellationToken cancellationToken)
    {
        await _redisDb.KeyDeleteAsync(key.Value);
        // Publish invalidation to other instances
        await _redisDb.PublishAsync(_invalidationChannel, key.Value);
        return Result.Success();
    }
}