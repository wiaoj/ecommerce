using ecommerce.Application.Common.Interfaces;

namespace ecommerce.Infrastructure.Services;
internal sealed class CacheKeyGenerator : ICacheKeyGenerator {
    private readonly IHashingProvider hashProvider;

    public CacheKeyGenerator(IHashingProvider hashProvider) {
        this.hashProvider = hashProvider;
    }

    public String Generate<T>(T request) {
        return $"{typeof(T).Name}-{this.hashProvider.Generate(request)}";
    }
}