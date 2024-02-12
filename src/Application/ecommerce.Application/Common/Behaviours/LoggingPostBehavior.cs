using ecommerce.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class LoggingPostBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : notnull {
    private readonly ILogger<LoggingPostBehavior<TRequest, TResponse>> logger;
    private readonly ICurrentUserProvider currentUserService;

    public LoggingPostBehavior(ILogger<LoggingPostBehavior<TRequest, TResponse>> logger, ICurrentUserProvider currentUserService) {
        this.logger = logger;
        this.currentUserService = currentUserService;
    }

    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken) {
        String requestName = typeof(TRequest).Name;
        String userId = this.currentUserService.UserId;
        String userName = this.currentUserService.UserName;

        this.logger.LogInformation("Processed request {RequestName} for {UserId} ({UserName}). Response details: {@Response}",
                                   requestName,
                                   userId,
                                   userName,
                                   response);

        return Task.CompletedTask;
    }
}