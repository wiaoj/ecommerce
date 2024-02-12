namespace ecommerce.Application.Common.Interfaces;
public interface ICacheKeyService {
    Task<String[]?> GetKeysAsync(String key, CancellationToken cancellationToken);
    Task RemoveKeyAsync(String key, CancellationToken cancellationToken);
    Task AddKeyAsync(String key, String keyToAdd, CancellationToken cancellationToken);
    Task AddKeysAsync(String key, String[] keysToAdd, CancellationToken cancellationToken);
}