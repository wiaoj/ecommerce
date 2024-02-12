using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Features.Authentication.Common;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Authentication.Exceptions;

namespace ecommerce.Application.Features.Authentication.Queries.RefreshTokenLogin;
internal sealed class RefreshTokenLoginQueryHandler : IRequestHandler<RefreshTokenLoginQuery, AuthenticationResponse> {
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IRefreshTokenGenerator refreshTokenGenerator;
    private readonly IUserFactory userFactory;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ICurrentUserService currentUserService;

    public RefreshTokenLoginQueryHandler(IUserRepository userRepository,
                                         IJwtTokenGenerator jwtTokenGenerator,
                                         IRefreshTokenGenerator refreshTokenGenerator,
                                         IUserFactory userFactory,
                                         IDateTimeProvider dateTimeProvider,
                                         ICurrentUserService currentUserService) {
        this.userRepository = userRepository;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.refreshTokenGenerator = refreshTokenGenerator;
        this.userFactory = userFactory;
        this.dateTimeProvider = dateTimeProvider;
        this.currentUserService = currentUserService;
    }

    public async Task<AuthenticationResponse> Handle(RefreshTokenLoginQuery request, CancellationToken cancellationToken) {
        UserAggregate user = await this.userRepository.FindByRefreshTokenAsync(request.RefreshToken, cancellationToken)
            ?? throw new UserNotFoundException();

        String token = this.jwtTokenGenerator.GenerateJwtToken(user);
        String refreshTokenValue = this.refreshTokenGenerator.GenerateRefreshToken();
        RefreshToken newRefreshToken = this.userFactory.CreateRefreshToken(refreshTokenValue,
                                                                           this.dateTimeProvider.Now,
                                                                           this.dateTimeProvider.Now.AddMinutes(20),
                                                                           this.currentUserService.UserIpAddress);
        user.UpdateRefreshToken(request.RefreshToken, newRefreshToken);
        await this.userRepository.UpdateAsync(user, cancellationToken);
        return new(token, refreshTokenValue);
    }
}