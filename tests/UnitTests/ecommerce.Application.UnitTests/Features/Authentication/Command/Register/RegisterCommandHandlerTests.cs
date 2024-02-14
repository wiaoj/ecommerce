using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Authentication.Commands.Register;
using ecommerce.Application.Features.Authentication.Common;
using ecommerce.Application.UnitTests.Features.Authentication.Extensions;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Application.UnitTests.Features.Authentication.Command.Register;
public sealed class RegisterCommandHandlerTests {
    private readonly RegisterCommandHandler handler;
    private readonly IUserFactory userFactory;
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IRefreshTokenGenerator refreshTokenGenerator;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ICurrentUserProvider currentUserService;

    public RegisterCommandHandlerTests() {
        this.userFactory = Substitute.For<IUserFactory>();
        this.userRepository = Substitute.For<IUserRepository>();
        this.jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        this.refreshTokenGenerator = Substitute.For<IRefreshTokenGenerator>();
        this.dateTimeProvider = Substitute.For<IDateTimeProvider>();
        this.currentUserService = Substitute.For<ICurrentUserProvider>();
        this.handler = new RegisterCommandHandler(this.userFactory,
                                             this.userRepository,
                                             this.jwtTokenGenerator,
                                             this.refreshTokenGenerator,
                                             this.dateTimeProvider,
                                             this.currentUserService);
    }

    [Theory]
    [ClassData(typeof(RegisterCommandHandlerTestsData))]
    public async Task HandleRegisterCommand_GivenValidRequest_ShouldReturnAuthenticationResponse(RegisterCommand command) {
        // Arrange
        String expectedJwtToken = "GeneratedJwtToken";
        String expectedRefreshTokenValue = "GeneratedRefreshTokenValue";
        UserAggregate expectedUser = UserTestFactory.CreateValidUserAggregate();
        RefreshToken expectedRefreshToken = UserTestFactory.CreateValidRefreshToken();

        this.jwtTokenGenerator.GenerateJwtToken(Arg.Any<UserAggregate>()).Returns(expectedJwtToken);
        this.refreshTokenGenerator.GenerateRefreshToken().Returns(expectedRefreshTokenValue);
        this.userFactory.CreateRefreshToken(Arg.Any<String>(),
                                            Arg.Any<DateTime>(),
                                            Arg.Any<DateTime>(),
                                            Arg.Any<String>())
            .Returns(expectedRefreshToken);

        this.userFactory.Create(Arg.Any<FullName>(),
                                Arg.Any<Email>(),
                                Arg.Any<PhoneNumber>(),
                                Arg.Any<Password>())
            .Returns(expectedUser);


        // Act
        AuthenticationResponse response = await this.handler.Handle(command, CancellationToken.None);

        // Assert
        response.Token.Should().Be(expectedJwtToken);
        response.RefreshToken.Should().Be(expectedRefreshTokenValue);
        expectedUser.ValidateRefreshToken(expectedRefreshToken);
    }

    [Theory]
    [ClassData(typeof(RegisterCommandHandlerTestsData))]
    public async Task HandleRegisterCommand_ShouldCreateUserCorrectly(RegisterCommand command) {
        // Arrange
        UserId expectedUserId = UserTestFactory.UserId;
        FullName expectedFullName = UserTestFactory.CreateExpectedFullName(command.FirstName, command.LastName);
        Email expectedEmail = UserTestFactory.CreateExpectedEmail(command.Email);
        PhoneNumber expectedPhoneNumber = UserTestFactory.CreateExpectedPhoneNumber(command.PhoneNumber);
        Password expectedPassword = UserTestFactory.CreateValidPassword();
        RefreshToken expectedRefreshToken = UserTestFactory.CreateValidRefreshToken();
        UserAggregate expectedUser = UserTestFactory.CreateExpectedUserAggregate(expectedUserId,
                                                                              expectedFullName,
                                                                              expectedEmail,
                                                                              expectedPhoneNumber,
                                                                              expectedPassword);

        this.userFactory.CreateFullName(Arg.Is<String>(command.FirstName), Arg.Is<String>(command.LastName))
            .Returns(expectedFullName);

        this.userFactory.CreateEmail(Arg.Is<String>(command.Email))
            .Returns(expectedEmail);

        this.userFactory.CreatePhoneNumber(Arg.Is<String?>(command.PhoneNumber))
            .Returns(expectedPhoneNumber);

        this.userFactory.CreatePassword(Arg.Is<String>(command.Password))
            .Returns(expectedPassword);

        this.userFactory.Create(Arg.Is<FullName>(expectedFullName),
                                Arg.Is<Email>(expectedEmail),
                                Arg.Is<PhoneNumber>(expectedPhoneNumber),
                                Arg.Is<Password>(expectedPassword))
            .Returns(expectedUser);


        this.userFactory.CreateRefreshToken(Arg.Any<String>(), Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<String>())
            .Returns(expectedRefreshToken);

        // Act 
        await this.handler.Handle(command, CancellationToken.None);

        // Assert
        this.userFactory.Received(1).CreateFullName(command.FirstName, command.LastName);
        this.userFactory.Received(1).CreateEmail(command.Email);
        this.userFactory.Received(1).CreatePhoneNumber(command.PhoneNumber);
        this.userFactory.Received(1).CreatePassword(command.Password);

        await this.userRepository.Received(1).CreateAsync(Arg.Is<UserAggregate>(expectedUser), Arg.Any<CancellationToken>());
        expectedUser.VerifyUserCreationFromCommand(command);
    }
}