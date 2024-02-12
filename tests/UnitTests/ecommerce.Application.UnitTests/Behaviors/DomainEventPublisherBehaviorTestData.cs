namespace ecommerce.Application.UnitTests.Behaviors;
public sealed class DomainEventPublisherBehaviorTestData : TheoryData<Int32, Int32> {
    public DomainEventPublisherBehaviorTestData() {
        Add(1, 1);
        Add(2, 2);
        Add(5, 5);
        Add(10, 10);
    }
}