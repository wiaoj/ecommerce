using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Application.Features.Categories.MappingExtensions;
using ecommerce.Application.UnitTests.Features.Categories.Commands.TestUtils;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.CreateCategory;
[Collection("CategoryCollection")]
public class CreateCategoryCommandHandlerTests {
    private readonly CreateCategoryCommandHandler handler;
    private readonly ICategoryRepository categoryRepository;
    private readonly ICategoryFactory categoryFactory;

    public CreateCategoryCommandHandlerTests() {
        this.categoryRepository = Substitute.For<ICategoryRepository>();
        this.categoryFactory = Substitute.For<ICategoryFactory>();
        this.handler = new CreateCategoryCommandHandler(this.categoryFactory, this.categoryRepository);
    }

    [Theory]
    [MemberData(nameof(ValidCreateCategoryCommands))]
    public async Task HandleCreateCategoryCommand_WhenCategoryIsValid_ShouldCreateAndReturnCategory(
        CreateCategoryCommand command) {
        // Arrange
        var expectedCategory = CategoryTestFactory.CreateValidCategoryAggregate();

        this.categoryFactory.FromCreateCommand(command).Returns(expectedCategory);

        // Act 
        CreateCategoryCommandResult result = await this.handler.Handle(command, CancellationToken.None);

        // Assert 
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedCategory.Id);
        result.ParentId.Should().Be(expectedCategory.ParentId);
        result.Name.Should().Be(expectedCategory.Name.Value);

        // Using FluentAssertions to validate the object creation
        result.Should().BeEquivalentTo(expectedCategory, options => options.ExcludingMissingMembers());

        // Verify that CreateAsync was called exactly once with any CategoryAggregate and CancellationToken
        await this.categoryRepository.Received(1).CreateAsync(Arg.Any<CategoryAggregate>(), Arg.Any<CancellationToken>());
    }

    public static IEnumerable<Object[]> ValidCreateCategoryCommands() {
        yield return new[] { CreateCategoryCommandUtils.CreateCommand() };
        yield return new[] { CreateCategoryCommandUtils.CreateCommandWithParent() };
    }
}