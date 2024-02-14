using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Application.UnitTests.Features.Categories.Commands.TestUtils;
using ecommerce.Application.UnitTests.TestUtils.Categories.Extensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using FluentAssertions;
using Moq;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.CreateCategory;
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
        // Act 
        CreateCategoryCommandResult result = await this.handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert 
        result.Should().NotBeNull();
        result.ValidateCreatedFrom(command);
        await this.categoryRepository.Received(1).CreateAsync(It.IsAny<CategoryAggregate>(), It.IsAny<CancellationToken>());
    }

    public static IEnumerable<Object[]> ValidCreateCategoryCommands() {
        yield return new[] { CreateCategoryCommandUtils.CreateCommand() };
        yield return new[] { CreateCategoryCommandUtils.CreateCommandWithParent() };
    }
}