using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate;

namespace ecommerce.Domain.UnitTests.Aggregates.Products.ProductFactoryTests;
public partial class ProductFactoryTests {
    private readonly IProductFactory factory;

    public ProductFactoryTests() {
        this.factory = new ProductFactory();
    }
}