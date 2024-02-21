using Bogus;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Constants;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Products.Extensions;

namespace ecommerce.UnitTests.Common.Products;
public static class ProductTestFactory {
    private static readonly Faker<ProductAggregate> productFaker = new();
    private static readonly Faker faker = new();

    public static ProductAggregate CreateProduct() {
        return productFaker.CreateProduct(0).Generate();
    }

    public static ProductId CreateProductId() {
        return new(Guid.NewGuid());
    }

    public static ProductId CreateProductId(Guid value) {
        return new(value);
    }

    public static ProductDescription CreateProductDescription() {
        return new(faker.Commerce.ProductDescription());
    }

    public static ProductDescription CreateProductDescription(String value) {
        return new(value);
    }

    public static ProductName CreateProductName() {
        return new(faker.Commerce.ProductName());
    }

    public static ProductName CreateProductName(String value) {
        return new(value);
    }
}