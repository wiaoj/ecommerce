using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Authentication.Common;
using ecommerce.Application.Features.Authentication.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Queries.Login;
internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResponse> {
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IRefreshTokenGenerator refreshTokenGenerator;
    private readonly IUserFactory userFactory;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ICurrentUserProvider currentUserService;
    private readonly IPasswordHasher passwordHasher;

    public LoginQueryHandler(IUserRepository userRepository,
                             IJwtTokenGenerator jwtTokenGenerator,
                             IRefreshTokenGenerator refreshTokenGenerator,
                             IUserFactory userFactory,
                             IDateTimeProvider dateTimeProvider,
                             ICurrentUserProvider currentUserService,
                             IPasswordHasher passwordHasher) {
        this.userRepository = userRepository;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.refreshTokenGenerator = refreshTokenGenerator;
        this.userFactory = userFactory;
        this.dateTimeProvider = dateTimeProvider;
        this.currentUserService = currentUserService;
        this.passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResponse> Handle(LoginQuery request, CancellationToken cancellationToken) {
        Email email = this.userFactory.CreateEmail(request.Email);
        UserAggregate user = await this.userRepository.FindByEmailAsync(email, cancellationToken)
            ?? throw new InvalidEmailException();

        if(this.passwordHasher.VerifyPassword(user.Password, request.Password).IsFalse())
            throw new InvalidPasswordException();

        String token = this.jwtTokenGenerator.GenerateJwtToken(user);
        String refreshTokenValue = this.refreshTokenGenerator.GenerateRefreshToken();
        RefreshToken refreshToken = this.userFactory.CreateRefreshToken(refreshTokenValue,
                                                                        this.dateTimeProvider.UtcNow,
                                                                        this.dateTimeProvider.UtcNow.AddMinutes(20),
                                                                        this.currentUserService.UserIpAddress);
        user.AddRefreshToken(refreshToken);
        await this.userRepository.UpdateAsync(user, cancellationToken);
        return new(token, refreshTokenValue);
    }
}