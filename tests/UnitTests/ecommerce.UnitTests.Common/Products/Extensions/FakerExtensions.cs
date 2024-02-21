using Bogus;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.UnitTests.Common.Products.Extensions;
internal static class FakerExtensions {
    public static Faker<ProductAggregate> CreateProduct(this Faker<ProductAggregate> faker,
                                                        Int32 variantCount) {
        Func<Faker, ProductAggregate> func = faker => new ProductAggregate(
            ProductTestFactory.CreateProductId(),
            CategoryTestFactory.CreateCategoryId(),
            ProductTestFactory.CreateProductName(faker.Commerce.ProductName()),
            ProductTestFactory.CreateProductDescription(faker.Commerce.ProductDescription()),
            []);
            //[.. Enumerable.Range(0, variantCount).Select(_ => ProductTestFactory.CreateProductVariant())]
        return faker.CustomInstantiator(func);
    }
}