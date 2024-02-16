using Bogus;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories.Extensions;

namespace ecommerce.UnitTests.Common.Categories;
public static class CategoryTestFactory {
    private static readonly Faker<CategoryAggregate> categoryFaker = new();
    private static readonly Faker faker = new();

    public static CategoryAggregate CreateCategory() {
        return CreateCategory(false);
    }

    public static CategoryAggregate CreateCategory(Boolean hasParentCategory) {
        return CreateCategory(hasParentCategory, 0, 0);
    }

    public static CategoryAggregate CreateCategory(Int32 childCategoryCount) {
        return CreateCategory(false, childCategoryCount, 0);
    }

    public static CategoryAggregate CreateCategory(Boolean hasParentCategory,
                                                   Int32 childCategoryCount,
                                                   Int32 productCount) {
        return categoryFaker.CreateCategory(hasParentCategory,
                                            childCategoryCount,
                                            productCount).Generate();
    }

    public static CategoryAggregate CreateCategoryWithProducts() {
        return CreateCategoryWithProducts(1);
    }

    public static CategoryAggregate CreateCategoryWithProducts(Int32 productCount) {
        return CreateCategory(false, 0, productCount);
    }

    public static CategoryId CreateCategoryId() {
        return new(faker.Random.Guid());
    }

    public static CategoryId CreateCategoryId(Guid value) {
        return new(value);
    }

    public static CategoryId CreateSubcategoryId() {
        return new(faker.Random.Guid());
    }

    public static CategoryId CreateParentCategoryId() {
        return new(faker.Random.Guid());
    }

    public static CategoryName CreateCategoryName() {
        return new(faker.Commerce.Categories(1)[0]);
    }

    public static CategoryName CreateCategoryName(String value) {
        return new(value);
    }
}