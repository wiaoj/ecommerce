using ecommerce.Application.Common.Behaviours;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Common.Exceptions;
using FluentAssertions;

namespace ecommerce.Application.UnitTests.Behaviors;
public class AuthorizationBehaviorTests {
    private readonly ICurrentUserProvider currentUserProvider;
    private readonly AuthorizationBehavior<IAuthorizeRequest> behavior;
    private readonly IAuthorizeRequest request;

    public AuthorizationBehaviorTests() {
        this.currentUserProvider = Substitute.For<ICurrentUserProvider>();
        this.behavior = new AuthorizationBehavior<IAuthorizeRequest>(this.currentUserProvider);
        this.request = Substitute.For<IAuthorizeRequest>();
    }

    [Fact]
    public async Task AuthorizationBehavior_WhenUserExists_ShouldProceedToNext() {
        // Arrange
        this.currentUserProvider.UserId.Returns(User.Id.ToString());

        // Act
        Func<Task> func = () => this.behavior.Process(this.request, CancellationToken.None);

        // Assert
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task AuthorizationBehavior_WhenUserDoesNotExist_ShouldThrowUnauthorizedException() {
        // Arrange
        this.currentUserProvider.UserId.Returns(String.Empty);

        // Act & Assert
        Func<Task> func = () => this.behavior.Process(this.request, CancellationToken.None);
        await func.Should().ThrowExactlyAsync<UnauthorizeException>();
    }
}