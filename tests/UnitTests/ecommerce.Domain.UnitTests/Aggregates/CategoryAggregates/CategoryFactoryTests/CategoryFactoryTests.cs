using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryFactoryTests;
public partial class CategoryFactoryTests {
    private readonly ICategoryFactory factory;

    public CategoryFactoryTests() {
        this.factory = new CategoryFactory();
    }
}