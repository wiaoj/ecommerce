using Bogus;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories.Extensions;

namespace ecommerce.UnitTests.Common.Categories;
public static class CategoryTestFactory {
    private static readonly Faker<CategoryAggregate> categoryFaker = new();
    private static readonly Faker faker = new();

    public static CategoryAggregate CreateValidCategoryAggregate() {
        return CreateValidCategoryAggregate(false);
    }

    public static CategoryAggregate CreateValidCategoryAggregate(Boolean hasParentCategory) {
        return CreateValidCategoryAggregate(hasParentCategory, 0, 0);
    }

    public static CategoryAggregate CreateValidCategoryAggregate(Int32 childCategoryCount) {
        return CreateValidCategoryAggregate(false, childCategoryCount, 0);
    }

    public static CategoryAggregate CreateValidCategoryAggregate(Boolean hasParentCategory,
                                                                 Int32 childCategoryCount,
                                                                 Int32 productCount) {
        return categoryFaker.CreateCategory(hasParentCategory, childCategoryCount, productCount).Generate();
    }

    public static CategoryAggregate CreateValidCategoryAggregateWithProducts() {
        return CreateValidCategoryAggregateWithProducts(1);
    }

    public static CategoryAggregate CreateValidCategoryAggregateWithProducts(Int32 productCount) {
        return CreateValidCategoryAggregate(false, 0, productCount);
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

    public static CategoryName CreateValidCategoryName() {
        return new(faker.Commerce.Categories(1)[0]);
    }

    public static CategoryName CreateValidCategoryName(String value) {
        return new(value);
    }

    private static CategoryId CreateCategoryId() {
        return new(faker.Random.Guid());
    }
}