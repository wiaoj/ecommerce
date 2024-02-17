using ecommerce.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class DistributedCacheInvalidationBehavior<TRequest, TResponse>
    : IRequestPostProcessor<TRequest, TResponse> where TRequest : IHasCacheInvalidation {
    //TODO: i cache key provider
    private readonly ICacheKeyService cacheKeyService;
    private readonly ILogger<DistributedCacheInvalidationBehavior<TRequest, TResponse>> logger;

    public DistributedCacheInvalidationBehavior(ICacheKeyService cacheKeyService,
                                                ILogger<DistributedCacheInvalidationBehavior<TRequest, TResponse>> logger) {
        this.cacheKeyService = cacheKeyService;
        this.logger = logger;
    }

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken) {
        this.logger.LogDebug("Invalidating cache for {CacheKey}", request.CacheKey);
        await this.cacheKeyService.RemoveKeyAsync(request.CacheKey, cancellationToken);
        this.logger.LogDebug("Invalidated cache for {CacheKey}", request.CacheKey);
    }
}