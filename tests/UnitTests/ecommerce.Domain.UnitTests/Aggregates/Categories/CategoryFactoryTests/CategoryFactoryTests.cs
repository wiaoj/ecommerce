using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Domain.UnitTests.Aggregates.Categories.CategoryFactoryTests;
public partial class CategoryFactoryTests {
    private readonly ICategoryFactory factory;

    public CategoryFactoryTests() {
        this.factory = new CategoryFactory();
    }
}