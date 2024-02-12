using ecommerce.Application.Common.Extensions;
using ecommerce.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ecommerce.Infrastructure.Services;
internal sealed class ApplicationDistributedCache : IApplicationDistributedCache, ICacheKeyService {
    private readonly IDistributedCache distributedCache;

    public ApplicationDistributedCache(IDistributedCache distributedCache) {
        this.distributedCache = distributedCache;
    }

    public Task<String?> GetStringAsync(String key, CancellationToken cancellationToken) {
        return this.distributedCache.GetStringAsync(key, cancellationToken);
    }

    public Task SetStringAsync(String key, String value, TimeSpan? absoluteExpireTime, CancellationToken cancellationToken) {
        return SetStringAsync(key, value, absoluteExpireTime, null, cancellationToken);
    }

    public async Task SetStringAsync(String key,
                                     String value,
                                     TimeSpan? absoluteExpireTime,
                                     TimeSpan? slidingExpireTime,
                                     CancellationToken cancellationToken) {
        DistributedCacheEntryOptions options = new();
        if(absoluteExpireTime.HasValue)
            options.SetAbsoluteExpiration(absoluteExpireTime.Value);

        if(slidingExpireTime.HasValue)
            options.SetSlidingExpiration(slidingExpireTime.Value);

        await this.distributedCache.SetStringAsync(key, value, options, cancellationToken);
    }

    public async Task SetAsync<T>(String key, T value, TimeSpan? absoluteExpireTime, CancellationToken cancellationToken) {
        DistributedCacheEntryOptions options = new();

        if(absoluteExpireTime.HasValue)
            options.SetAbsoluteExpiration(absoluteExpireTime.Value);

        Byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value);
        await this.distributedCache.SetAsync(key, bytes, options, cancellationToken);
    }

    public async Task<T?> GetAsync<T>(String key, CancellationToken cancellationToken) {
        Byte[]? bytes = await this.distributedCache.GetAsync(key, cancellationToken);

        if(bytes is null)
            return default;

        await using MemoryStream memoryStream = new(bytes);
        return await JsonSerializer.DeserializeAsync<T>(memoryStream, cancellationToken: cancellationToken);
    }

    public Task RemoveAsync(String key, CancellationToken cancellationToken) {
        return this.distributedCache.RemoveAsync(key, cancellationToken);
    }

    public Task<String[]?> GetKeysAsync(String key, CancellationToken cancellationToken) {
        return GetAsync<String[]>(key, cancellationToken);
    }

    public async Task AddKeysAsync(String key, String[] keysToAdd, CancellationToken cancellationToken) {
        String[] cachedKeys = await GetKeysAsync(key, cancellationToken) ?? [];

        IEnumerable<String> newKeys = keysToAdd.Except(cachedKeys);

        if(newKeys.CountIsZero())
            return;

        IEnumerable<String> updatedKeys = cachedKeys.Concat(newKeys);

        Byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(updatedKeys);
        await this.distributedCache.SetAsync(key, bytes, cancellationToken);
    }

    public async Task AddKeyAsync(String key, String keyToAdd, CancellationToken cancellationToken) {
        String[] cachedKeys = await GetKeysAsync(key, cancellationToken) ?? [];

        if(cachedKeys.Contains(keyToAdd))
            return;

        IEnumerable<String> updatedKeys = cachedKeys.Concat(new[] { keyToAdd }).Distinct();

        Byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(updatedKeys);
        await this.distributedCache.SetAsync(key, bytes, cancellationToken);
    }

    public Task RemoveKeyAsync(String key, CancellationToken cancellationToken) {
        return RemoveAsync(key, cancellationToken);
    }
}