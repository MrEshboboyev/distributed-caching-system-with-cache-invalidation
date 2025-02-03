using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;
using Domain.ValueObjects;

namespace Infrastructure.Security;

public sealed class CacheStrategy(
    ICacheRepository cacheRepository
) : ICacheStrategy
{
    public CacheStrategyType StrategyType => CacheStrategyType.CacheAside;

    public async Task<Result<CachedItem>> ReadThroughAsync(
        CacheKey key,
        Func<Task<Result<byte[]>>> fallbackDataSource,
        CacheExpiration expiration,
        CancellationToken cancellationToken)
    {
        // 1. Check cache
        var cachedItemResult = await cacheRepository.GetAsync(key, cancellationToken);
        if (cachedItemResult.IsSuccess)
            return cachedItemResult;

        // 2. Fallback to database
        var dataResult = await fallbackDataSource();
        if (dataResult.IsFailure)
            return Result.Failure<CachedItem>(dataResult.Error);

        // 3. Update cache
        var newItem = CachedItem.Create(
            Guid.NewGuid(),
            key.Value,
            dataResult.Value,
            expiration);
        await cacheRepository.SetAsync(newItem, cancellationToken);
        return newItem;
    }

    public async Task<Result> WriteThroughAsync(
        CacheKey key,
        byte[] value,
        CacheExpiration expiration,
        Func<Task<Result>> updateDataSource,
        CancellationToken cancellationToken)
    {
        // 1. Update database
        var updateResult = await updateDataSource();
        if (updateResult.IsFailure)
            return updateResult;

        // 2. Update cache
        var item = CachedItem.Create(
            Guid.NewGuid(),
            key.Value,
            value,
            expiration);;
        return await cacheRepository.SetAsync(item, cancellationToken);
    }
}