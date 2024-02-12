namespace ecommerce.Domain.Aggregates.StoreAggregate.ValueObjects;

public sealed record StoreName
{
    public string Value { get; private set; }

    public StoreName(string value)
    {
        Value = value;
    }
}