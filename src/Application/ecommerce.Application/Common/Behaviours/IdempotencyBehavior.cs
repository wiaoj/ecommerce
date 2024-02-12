using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Extensions;
using MediatR.Pipeline;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class IdempotencyBehavior<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : IHasIdemponency {
    private readonly IApplicationDistributedCache applicationDistributedCache;

    public IdempotencyBehavior(IApplicationDistributedCache applicationDistributedCache) {
        this.applicationDistributedCache = applicationDistributedCache;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken) {
        String? key = await this.applicationDistributedCache.GetStringAsync(request.RequestId.ToString(), cancellationToken);

        if(key.IsNotNullOrEmpty())
            throw new InvalidOperationException("This request has already been processed.");

        await this.applicationDistributedCache.SetStringAsync(request.RequestId.ToString(),
                                                         "Request has been processed.",
                                                         TimeSpan.FromMinutes(1),
                                                         cancellationToken);
    }
}