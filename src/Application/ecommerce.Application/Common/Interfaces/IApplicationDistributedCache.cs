namespace ecommerce.Application.Common.Interfaces;
public interface IApplicationDistributedCache {
    Task<T?> GetAsync<T>(String key, CancellationToken cancellationToken);
    Task<String?> GetStringAsync(String key, CancellationToken cancellationToken);
    Task SetAsync<T>(String key, T value, TimeSpan? absoluteExpireTime, CancellationToken cancellationToken);
    Task SetStringAsync(String key,
                        String value,
                        TimeSpan? absoluteExpireTime,
                        CancellationToken cancellationToken);
    Task SetStringAsync(String key,
                        String value,
                        TimeSpan? absoluteExpireTime,
                        TimeSpan? slidingExpireTime,
                        CancellationToken cancellationToken);
    Task RemoveAsync(String key, CancellationToken cancellationToken);
}