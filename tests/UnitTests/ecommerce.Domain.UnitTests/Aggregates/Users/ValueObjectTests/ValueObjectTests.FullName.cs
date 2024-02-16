using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.ValueObjectTests;
public sealed partial class ValueObjectTests {
    [Fact]
    public void FullName_ToWesternFormat_ShouldReturnCorrectFormat() {
        // Arrange
        FullName fullName = UserTestFactory.CreateFullName();
        String expectedFormat = $"{fullName.FirstName} {fullName.LastName}";

        // Act
        String westernFormat = fullName.ToWesternFormat();

        // Assert
        westernFormat.Should().Be(expectedFormat);
    }

    [Fact]
    public void FullName_ToEasternFormat_ShouldReturnCorrectFormat() {
        // Arrange
        FullName fullName = UserTestFactory.CreateFullName();
        String expectedFormat = $"{fullName.LastName} {fullName.FirstName}";

        // Act
        String easternFormat = fullName.ToEasternFormat();

        // Assert
        easternFormat.Should().Be(expectedFormat);
    }
}