using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Application.Features.Categories.Commands.DeleteCategory;
using ecommerce.Application.UnitTests.Features.Categories.Commands.TestUtils;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using FluentAssertions;
using Moq;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandHandlerTests
{
    private readonly DeleteCategoryCommandHandler handler;
    private readonly Mock<ICategoryRepository> mockCategoryRepository;
    private readonly ICategoryFactory categoryFactory;
    private readonly Mock<IGuardClause> mockGardClause;

    public DeleteCategoryCommandHandlerTests()
    {
        mockCategoryRepository = new Mock<ICategoryRepository>();
        categoryFactory = new CategoryFactory();
        mockGardClause = new Mock<IGuardClause>();
        handler = new DeleteCategoryCommandHandler(mockCategoryRepository.Object, mockGardClause.Object);
    }

    [Theory]
    [MemberData(nameof(ValidDeleteCategoryCommands))]
    public async Task HandleDeleteCategoryCommand_WhenCategoryIsValid_ShouldDeleteCategory(DeleteCategoryCommand command)
    {
        //arrange 
        CategoryAggregate category = Category.CreateValidCategory();
        mockCategoryRepository.Setup(repo => repo.FindByIdAsync(CategoryId.Create(command.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        // Act 
        await handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert   
        mockCategoryRepository.Verify(
            repository => repository.DeleteAsync(category, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory]
    [MemberData(nameof(ValidDeleteCategoryCommands))]
    public async Task HandleDeleteCategoryCommand_WhenCategoryIsNotFound_ShouldThrowException(DeleteCategoryCommand command)
    {
        // Arrange
        mockCategoryRepository.Setup(repo => repo.FindByIdAsync(CategoryId.Create(command.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        mockGardClause.Setup(clause
            => clause.ThrowIfNull<CategoryAggregate, CategoryNotFoundException>(null, It.IsAny<CategoryNotFoundException>()))
            .Throws<CategoryNotFoundException>();

        // Act & Assert
        await Assert.ThrowsAsync<CategoryNotFoundException>(() => handler.Handle(command, It.IsAny<CancellationToken>()));
    }

    public static IEnumerable<object[]> ValidDeleteCategoryCommands()
    {
        yield return new[] { DeleteCategoryCommandUtils.CreateCommand() };
    }
}