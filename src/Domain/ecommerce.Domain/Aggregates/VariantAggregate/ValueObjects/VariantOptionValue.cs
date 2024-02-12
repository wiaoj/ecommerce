namespace ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
public sealed record VariantOptionValue {
    public String Value { get; private set; }

    private VariantOptionValue() { }
    private VariantOptionValue(String value) {
        this.Value = value.ToUpper();
    }

    public static VariantOptionValue Create(String value) {
        return new(value);
    }
}