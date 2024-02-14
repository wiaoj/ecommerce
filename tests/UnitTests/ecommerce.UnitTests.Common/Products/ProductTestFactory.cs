using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;

namespace ecommerce.UnitTests.Common.Products;
public static class ProductTestFactory {
    public static ProductId CreateValidProductId() {
        return new(Guid.NewGuid());
    }

    public static ProductId CreateValidProductId(Guid value) {
        return new(value);
    }
}