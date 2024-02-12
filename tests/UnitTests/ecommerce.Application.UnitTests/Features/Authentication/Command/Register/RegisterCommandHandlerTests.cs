using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Authentication.Commands.Register;
using ecommerce.Application.Features.Authentication.Common;
using ecommerce.Application.UnitTests.TestUtils.Users.Extensions;
using ecommerce.Application.UnitTests.TestUtils.Users.Fakers;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using FluentAssertions;

namespace ecommerce.Application.UnitTests.Features.Authentication.Command.Register;
public sealed class RegisterCommandHandlerTests {
    private readonly RegisterCommandHandler handler;
    private readonly IUserFactory userFactory;
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IRefreshTokenGenerator refreshTokenGenerator;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ICurrentUserService currentUserService;

    public RegisterCommandHandlerTests() {
        this.userFactory = Substitute.For<IUserFactory>();
        this.userRepository = Substitute.For<IUserRepository>();
        this.jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        this.refreshTokenGenerator = Substitute.For<IRefreshTokenGenerator>();
        this.dateTimeProvider = Substitute.For<IDateTimeProvider>();
        this.currentUserService = Substitute.For<ICurrentUserService>();
        this.handler = new RegisterCommandHandler(this.userFactory,
                                             this.userRepository,
                                             this.jwtTokenGenerator,
                                             this.refreshTokenGenerator,
                                             this.dateTimeProvider,
                                             this.currentUserService);
    }

    [Theory]
    [ClassData(typeof(RegisterCommandHandlerTestData))]
    public async Task HandleRegisterCommand_GivenValidRequest_ShouldReturnAuthenticationResponse(RegisterCommand command) {
        // Arrange
        String expectedJwtToken = "GeneratedJwtToken";
        String expectedRefreshTokenValue = "GeneratedRefreshTokenValue";
        UserAggregate expectedUser = UserFaker.CreateValidUserAggregate();
        RefreshToken expectedRefreshToken = UserFaker.CreateValidRefreshToken();

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
    [ClassData(typeof(RegisterCommandHandlerTestData))]
    public async Task HandleRegisterCommand_ShouldCreateUserCorrectly(RegisterCommand command) {
        // Arrange
        UserId expectedUserId = UserFaker.UserId;
        FullName expectedFullName = new(command.FirstName, [], command.LastName);
        Email expectedEmail = new(command.Email);
        PhoneNumber expectedPhoneNumber = new(command.PhoneNumber);
        Password expectedPassword = new(command.Password, Guid.NewGuid().ToString());

        RefreshToken expectedRefreshToken = UserFaker.CreateValidRefreshToken();

        UserAggregate expectedUser = new(expectedUserId,
                                         expectedFullName,
                                         expectedEmail,
                                         expectedPhoneNumber,
                                         expectedPassword,
                                         []);

        this.userFactory.CreateFullName(Arg.Any<String>(), Arg.Any<String>()).Returns(expectedFullName);
        this.userFactory.CreateEmail(Arg.Any<String>()).Returns(expectedEmail);
        this.userFactory.CreatePhoneNumber(Arg.Any<String>()).Returns(expectedPhoneNumber);
        this.userFactory.CreatePassword(Arg.Any<String>()).Returns(expectedPassword);

        this.userFactory.Create(
            Arg.Is<FullName>(fn => fn.FirstName == command.FirstName && fn.LastName == command.LastName),
            Arg.Is<Email>(e => e.Value == command.Email),
            Arg.Is<PhoneNumber>(pn => pn.Value == command.PhoneNumber),
            Arg.Is<Password>(p => !String.IsNullOrEmpty(p.HashedValue) && !String.IsNullOrEmpty(p.Salt)))
        .Returns(expectedUser);

        this.userFactory.CreateRefreshToken(Arg.Any<String>(), Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<String>())
            .Returns(expectedRefreshToken);

        // Act 
        await this.handler.Handle(command, CancellationToken.None);

        // Assert
        expectedUser.VerifyUserCreationFromCommand(command);
        await this.userRepository.Received(1).CreateAsync(Arg.Is<UserAggregate>(user =>
                    user.FullName.FirstName == command.FirstName
                    && user.FullName.LastName == command.LastName
                    && user.Email.Value == command.Email
                    && user.PhoneNumber.Value == command.PhoneNumber
                    && user.Password.HashedValue == expectedPassword.HashedValue), Arg.Any<CancellationToken>());
    }
}