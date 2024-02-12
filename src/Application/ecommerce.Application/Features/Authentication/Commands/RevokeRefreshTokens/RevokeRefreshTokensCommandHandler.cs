using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Authentication.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Commands.RevokeRefreshTokens;
internal sealed class RevokeRefreshTokensCommandHandler : IRequestHandler<RevokeRefreshTokensCommand> {
    private readonly ICurrentUserProvider currentUserService;
    private readonly IUserRepository userRepository;

    public RevokeRefreshTokensCommandHandler(ICurrentUserProvider currentUserService, IUserRepository userRepository) {
        this.currentUserService = currentUserService;
        this.userRepository = userRepository;
    }

    public async Task Handle(RevokeRefreshTokensCommand request, CancellationToken cancellationToken) {
        UserId userId = UserId.Create(this.currentUserService.UserId);
        UserAggregate user = await this.userRepository.FindByIdAsync(userId, cancellationToken)
            ?? throw new UserNotFoundException();

        user.RevokeRefreshTokens();

        await this.userRepository.UpdateAsync(user, cancellationToken);
    }
}