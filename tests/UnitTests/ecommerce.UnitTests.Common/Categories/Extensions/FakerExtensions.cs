using Bogus;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Products;
using System;

namespace ecommerce.UnitTests.Common.Categories.Extensions;
internal static class FakerExtensions {
    public static Faker<CategoryAggregate> CreateCategory(this Faker<CategoryAggregate> faker,
                                                          Boolean hasParentCategory,
                                                          Int32 childCategoryCount,
                                                          Int32 productCount) {
        Func<Faker, CategoryAggregate> func = faker => new CategoryAggregate(
            hasParentCategory ? CategoryTestFactory.CreateCategoryId(faker.Random.Guid()) : null,
            CategoryTestFactory.CreateCategoryId(),
            CategoryTestFactory.CreateCategoryName(faker.Commerce.Categories(1)[0]),
            [.. Enumerable.Range(0, childCategoryCount).Select(_ => CategoryTestFactory.CreateCategoryId(faker.Random.Guid()))],
            [.. Enumerable.Range(0, productCount).Select(_ => ProductTestFactory.CreateValidProductId(faker.Random.Guid()))]);
        return faker.CustomInstantiator(func);
    }
}