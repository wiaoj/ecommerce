using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.UserFactoryTests;
public partial class UserFactoryTests {
    [Theory]
    [InlineData(null)]
    [InlineData("5551112233")]
    public void Create_ValidPhoneNumber_ShouldReturnPhoneNumber(String? phoneNumber) {
        // Act
        PhoneNumber createdPhoneNumber = this.userFactory.CreatePhoneNumber(phoneNumber);
        // Assert
        createdPhoneNumber.Value.Should().Be(phoneNumber);
        createdPhoneNumber.IsConfirmed.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreatePhoneNumber_ValueIsNullOrEmpty_ShouldNotThrow(String? phoneNumber) {
        // Act
        Func<PhoneNumber> func = () => this.userFactory.CreatePhoneNumber(phoneNumber);
        // Assert
        func.Should().NotThrow<InvalidPhoneNumberException>();
    }
}