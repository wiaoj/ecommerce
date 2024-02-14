using Bogus;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate;

namespace ecommerce.UnitTests.Common.Categories;
public static class CategoryTestFactory {
    public static CategoryAggregate CreateValidCategoryAggregate() {
        Faker<CategoryAggregate> userFaker = new Faker<CategoryAggregate>()
            .CustomInstantiator(faker => new CategoryAggregate(
                null,
                CategoryId.CreateUnique,
                CategoryName.Create(faker.Commerce.Categories(1).First()),
                []
            ));
        return userFaker.Generate();
    }
}