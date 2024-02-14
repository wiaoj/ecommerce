using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Authentication.Common;
using ecommerce.Application.Features.Authentication.MappingExtensions;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Commands.Register;
internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResponse> {
    private readonly IUserFactory userFactory;
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IRefreshTokenGenerator refreshTokenGenerator;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ICurrentUserProvider currentUserService;

    public RegisterCommandHandler(IUserFactory userFactory,
                                  IUserRepository userRepository,
                                  IJwtTokenGenerator jwtTokenGenerator,
                                  IRefreshTokenGenerator refreshTokenGenerator,
                                  IDateTimeProvider dateTimeProvider,
                                  ICurrentUserProvider currentUserService) {
        this.userFactory = userFactory;
        this.userRepository = userRepository;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.refreshTokenGenerator = refreshTokenGenerator;
        this.dateTimeProvider = dateTimeProvider;
        this.currentUserService = currentUserService;
    }

    public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken) {
        UserAggregate user = this.userFactory.FromRegisterCommand(request);

        String jwtToken = this.jwtTokenGenerator.GenerateJwtToken(user);
        String refreshTokenValue = this.refreshTokenGenerator.GenerateRefreshToken();
        RefreshToken refreshToken = this.userFactory.CreateRefreshToken(refreshTokenValue,
                                                                        this.dateTimeProvider.UtcNow,
                                                                        this.dateTimeProvider.UtcNow.AddMinutes(20),
                                                                        this.currentUserService.UserIpAddress);
        user.AddRefreshToken(refreshToken);
        await this.userRepository.CreateAsync(user, cancellationToken);
        return new(jwtToken, refreshTokenValue);
    }
}