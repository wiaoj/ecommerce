using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Authentication.Common;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Commands.Register;
internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResponse> {
    private readonly IUserFactory userFactory;
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IRefreshTokenGenerator refreshTokenGenerator;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ICurrentUserService currentUserService;

    public RegisterCommandHandler(IUserFactory userFactory,
                                  IUserRepository userRepository,
                                  IJwtTokenGenerator jwtTokenGenerator,
                                  IRefreshTokenGenerator refreshTokenGenerator,
                                  IDateTimeProvider dateTimeProvider,
                                  ICurrentUserService currentUserService) {
        this.userFactory = userFactory;
        this.userRepository = userRepository;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.refreshTokenGenerator = refreshTokenGenerator;
        this.dateTimeProvider = dateTimeProvider;
        this.currentUserService = currentUserService;
    }

    public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken) {
        FullName fullName = this.userFactory.CreateFullName(request.FirstName, request.LastName);
        Email email = this.userFactory.CreateEmail(request.Email);
        PhoneNumber phoneNumber = this.userFactory.CreatePhoneNumber(request.PhoneNumber);
        Password password = this.userFactory.CreatePassword(request.Password);
        UserAggregate user = this.userFactory.Create(fullName, email, phoneNumber, password);

        String jwtToken = this.jwtTokenGenerator.GenerateJwtToken(user);
        String refreshTokenValue = this.refreshTokenGenerator.GenerateRefreshToken();
        RefreshToken refreshToken = this.userFactory.CreateRefreshToken(refreshTokenValue,
                                                                        this.dateTimeProvider.Now,
                                                                        this.dateTimeProvider.Now.AddMinutes(20),
                                                                        this.currentUserService.UserIpAddress);
        user.AddRefreshToken(refreshToken);
        await this.userRepository.CreateAsync(user, cancellationToken);
        return new(jwtToken, refreshTokenValue);
    }
}