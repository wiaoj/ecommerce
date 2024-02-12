using ecommerce.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class LoggingPreBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull {
    private readonly ILogger<LoggingPreBehavior<TRequest>> logger;
    private readonly ICurrentUserProvider currentUserService;

    public LoggingPreBehavior(ILogger<LoggingPreBehavior<TRequest>> logger, ICurrentUserProvider currentUserService) {
        this.logger = logger;
        this.currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken) {
        String requestName = typeof(TRequest).Name;
        String userId = this.currentUserService.UserId;
        String userName = this.currentUserService.UserName;

        this.logger.LogInformation("Processing request: {RequestName} by {UserId} ({UserName}). Request details: {@Request}",
                                   requestName,
                                   userId,
                                   userName,
                                   request);

        return Task.CompletedTask;
    }
}