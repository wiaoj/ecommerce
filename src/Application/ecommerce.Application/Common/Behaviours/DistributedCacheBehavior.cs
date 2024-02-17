using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class DistributedCacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IHasDistributedCache {
    private readonly IApplicationDistributedCache applicationDistributedCache;
    private readonly ICacheKeyService cacheKeyService;
    private readonly ICacheKeyGenerator cacheKeyGenerator;
    private readonly ILogger<DistributedCacheBehavior<TRequest, TResponse>> logger;

    public DistributedCacheBehavior(IApplicationDistributedCache applicationDistributedCache,
                                    ICacheKeyService cacheKeyService,
                                    ICacheKeyGenerator cacheKeyGenerator,
                                    ILogger<DistributedCacheBehavior<TRequest, TResponse>> logger) {
        this.applicationDistributedCache = applicationDistributedCache;
        this.cacheKeyService = cacheKeyService;
        this.cacheKeyGenerator = cacheKeyGenerator;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        String cacheKey = this.cacheKeyGenerator.Generate(request);
        this.logger.LogDebug("Checking cache for key: {CacheKey}.", cacheKey);
        TResponse? cachedResponse = await this.applicationDistributedCache.GetAsync<TResponse>(cacheKey, cancellationToken);

        if(cachedResponse.NotNull()) {
            this.logger.LogDebug("Cache hit for key: {CacheKey}. Returning cached response.", cacheKey);
            return cachedResponse;
        }

        this.logger.LogDebug("Cache miss for key: {CacheKey}. Proceeding to fetch data.", cacheKey);

        TResponse? response = await next();
        await this.applicationDistributedCache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(1), cancellationToken);
        this.logger.LogDebug("Response for key: {CacheKey} cached successfully for 1 minute.", cacheKey);

        await this.cacheKeyService.AddKeyAsync(request.CacheKey, cacheKey, cancellationToken);
        this.logger.LogDebug("Cache key management updated. Request key: {RequestCacheKey} associated with cache key: {CacheKey}.",
                                   request.CacheKey,
                                   cacheKey);

        return response;
    }
}