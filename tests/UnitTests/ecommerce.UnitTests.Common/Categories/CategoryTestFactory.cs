using Bogus;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;

namespace ecommerce.UnitTests.Common.Categories;
public static class CategoryTestFactory {
    public static CategoryAggregate CreateValidCategoryAggregate() {
        return CreateValidCategoryAggregate(false);
    }

    public static CategoryAggregate CreateValidCategoryAggregate(Boolean hasParentCategory) {
        return CreateValidCategoryAggregate(hasParentCategory, 0, 0);
    }

    public static CategoryAggregate CreateValidCategoryAggregate(Int32 childCategoryCount) {
        return CreateValidCategoryAggregate(false, childCategoryCount, 0);
    }

    public static CategoryAggregate CreateValidCategoryAggregateWithProducts() {
        return CreateValidCategoryAggregateWithProducts(1);
    }

    public static CategoryAggregate CreateValidCategoryAggregateWithProducts(Int32 productCount) {
        return CreateValidCategoryAggregate(false, 0, productCount);
    }

    public static CategoryAggregate CreateValidCategoryAggregate(Boolean hasParentCategory,
                                                                 Int32 childCategoryCount,
                                                                 Int32 productCount) {
        CategoryId? parentCategoryId = hasParentCategory ? CreateValidParentCategoryId() : null;
        Faker<CategoryAggregate> userFaker = new Faker<CategoryAggregate>()
            .CustomInstantiator(faker => new CategoryAggregate(
                parentCategoryId,
                CategoryId.CreateUnique,
                new CategoryName(faker.Commerce.Categories(1)[0]),
                Enumerable.Range(0, childCategoryCount).Select(_ => new CategoryId(faker.Random.Guid())).ToList(),
                Enumerable.Range(0, productCount).Select(_ => new ProductId(faker.Random.Guid())).ToList()
            ));
        return userFaker.Generate();
    }

    public static CategoryId CreateValidCategoryId() {
        return CreateCategoryId();
    }

    public static CategoryId CreateValidCategoryId(Guid value) {
        return new(value);
    }

    public static CategoryId CreateValidSubcategoryId() {
        return CreateCategoryId();
    }

    public static CategoryId CreateValidParentCategoryId() {
        return CreateCategoryId();
    }

    private static CategoryId CreateCategoryId() => new(Guid.NewGuid());
}