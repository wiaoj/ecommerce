using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.UserFactoryTests;
public partial class UserFactoryTests {
    [Theory]
    [InlineData("John", "Doe")]
    public void Create_ValidFullName_ShouldReturnFullName(String firstName, String lastName) {
        // Act
        FullName fullName = this.userFactory.CreateFullName(firstName, lastName);
        // Assert
        fullName.FirstName.Should().Be(firstName);
        fullName.LastName.Should().Be(lastName);
    }

    [Theory]
    [InlineData("", "LastName")]
    [InlineData(" ", "LastName")]
    [InlineData("          ", "LastName")]
    [InlineData(null, "LastName")]
    [InlineData("J", "Doe")]
    [InlineData("John123", "Doe")]
    [InlineData("John*", "Doe")]
    public void CreateFullName_WithInvalidFirstName_ShouldThrowInvalidFirstNameException(String? firstName, String lastName) {
        // Act & Assert
        this.userFactory.Invoking(x => x.CreateFullName(firstName!, lastName))
            .Should()
            .ThrowExactly<InvalidFirstNameFormatException >()
            .WithMessage(UserConstants.ErrorMessages.InvalidFirstNameCharacters.Format(firstName));
    }

    [Theory]
    [InlineData("FirstName", "")]
    [InlineData("FirstName", " ")]
    [InlineData("FirstName", "          ")]
    [InlineData("FirstName", null)]
    [InlineData("John", "D")]
    [InlineData("John", "Doe1")]
    [InlineData("John", "Doe!")]
    public void CreateFullName_WithInvalidLastName_ShouldThrowInvalidLastNameException(String firstName, String? lastName) {
        // Act & Assert
        this.userFactory.Invoking(x => x.CreateFullName(firstName, lastName!))
            .Should()
            .ThrowExactly<InvalidLastNameFormatException>()
            .WithMessage(UserConstants.ErrorMessages.InvalidLastNameCharacters.Format(lastName));
    }
}