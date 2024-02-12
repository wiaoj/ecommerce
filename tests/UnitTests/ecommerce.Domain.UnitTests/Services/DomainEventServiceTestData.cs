namespace ecommerce.Domain.UnitTests.Services;
public sealed class DomainEventServiceTestData : TheoryData<Int32> {
    public DomainEventServiceTestData() {
        Add(1);
        Add(5);
        Add(10);
    }
}