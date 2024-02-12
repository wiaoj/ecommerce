using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Products.Commands.CreateProductVariant;
using ecommerce.Application.UnitTests.Features.Products.Commands.TestUtils;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using Moq;

namespace ecommerce.Application.UnitTests.Features.Products.Commands.CreateProductVariant;
public sealed class CreateProductVariantCommandHandlerTests
{
    private readonly CreateProductVariantCommandHandler handler;
    private readonly Mock<IProductRepository> productRepository;
    private readonly Mock<IProductFactory> productFactory;
    private readonly Mock<IGuardClause> guardClause;

    public CreateProductVariantCommandHandlerTests()
    {
        productRepository = new Mock<IProductRepository>();
        productFactory = new Mock<IProductFactory>();
        guardClause = new Mock<IGuardClause>();
        handler = new(productFactory.Object, productRepository.Object, guardClause.Object);
    }

    [Theory]
    [MemberData(nameof(ValidCreateProductVariantCommands))]
    public async Task HandleCreateProductVariantCommand_WhenProductVariantIsValid_ShouldCreateAndReturnProductVariant(
    CreateProductVariantCommand command)
    {
        // Arrange
        ProductAggregate existingProduct = new(
            ProductId.Create(command.ProductId),
            CategoryId.Create(Category.Id),
            ProductName.Create("Test Product"),
            ProductDescription.Create("Test Description"),
            []);

        productRepository.Setup(repo => repo.FindByIdAsync(command.ProductId, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(existingProduct);

        ProductVariantEntity newVariant = new(
            ProductVariantId.CreateUnique,
            ProductId.Create(command.ProductId),
            "sku",
             command.Stock,
            ProductVariantPrice.Create(command.Price),
            command.Options.Select(VariantOptionId.Create).ToList());

        productFactory.Setup(factory => factory.CreateVariant(
            command.ProductId,
            command.Stock,
            command.Price,
            command.Options))
                      .Returns(newVariant);

        // Act
        await handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        productRepository.Verify(repo => repo.UpdateAsync(It.IsAny<ProductAggregate>(), It.IsAny<CancellationToken>()), Times.Once);
        productFactory.Verify(factory => factory.CreateVariant(
            command.ProductId,
            command.Stock,
            command.Price,
            command.Options), Times.Once);

        // Additional assertions to verify that the variant was added to the product
        Assert.Contains(newVariant, existingProduct.Variants);
    }

    public static IEnumerable<object[]> ValidCreateProductVariantCommands()
    {
        yield return new[] { CreateProductVariantCommandUtils.CreateVariantCommand() };
    }
}