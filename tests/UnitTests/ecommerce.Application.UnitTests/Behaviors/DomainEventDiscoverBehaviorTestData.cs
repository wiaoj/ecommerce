namespace ecommerce.Application.UnitTests.Behaviors;
public sealed class DomainEventDiscoverBehaviorTestData : TheoryData<Int32> {
    public DomainEventDiscoverBehaviorTestData() {
        Add(1);
        Add(2);
        Add(5);
        Add(10);
    }
}