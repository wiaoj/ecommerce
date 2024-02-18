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
public sealed class CreateProductVariantCommandHandlerTests {
    private readonly CreateProductVariantCommandHandler handler;
    private readonly IProductRepository productRepository;
    private readonly IProductFactory productFactory;

    public CreateProductVariantCommandHandlerTests() {
        this.productRepository = Substitute.For<IProductRepository>();
        this.productFactory = Substitute.For<IProductFactory>();
        this.handler = new(this.productFactory, this.productRepository);
    }

    [Theory]
    [MemberData(nameof(ValidCreateProductVariantCommands))]
    public async Task HandleCreateProductVariantCommand_WhenProductVariantIsValid_ShouldCreateAndReturnProductVariant(CreateProductVariantCommand command) {
        // Arrange
        ProductAggregate existingProduct = new(
            ProductId.Create(command.ProductId),
            CategoryId.Create(Category.Id),
            ProductName.Create("Test Product"),
            ProductDescription.Create("Test Description"),
            []);

        this.productRepository.FindByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
                         .Returns(existingProduct);

        ProductVariantEntity newVariant = new(
            ProductVariantId.CreateUnique,
            ProductId.Create(command.ProductId),
            "sku",
             command.Stock,
            ProductVariantPrice.Create(command.Price),
            command.Options.Select(VariantOptionId.Create).ToList());

        this.productFactory.CreateVariant(command.ProductId,
                                          command.Stock,
                                          command.Price,
                                          command.Options)
            .Returns(newVariant);

        // Act
        await this.handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        await this.productRepository.Received(1).UpdateAsync(Arg.Any<ProductAggregate>(), Arg.Any<CancellationToken>());
        this.productFactory.Received(1).CreateVariant(command.ProductId,
                                                      command.Stock,
                                                      command.Price,
                                                      command.Options);

        // Additional assertions to verify that the variant was added to the product
        existingProduct.Variants.Should().Contain(newVariant);
    }

    public static IEnumerable<Object[]> ValidCreateProductVariantCommands() {
        yield return new[] { CreateProductVariantCommandUtils.CreateVariantCommand() };
    }
}