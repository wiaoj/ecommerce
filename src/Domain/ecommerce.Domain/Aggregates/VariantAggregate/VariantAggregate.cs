using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate.Entities;
using ecommerce.Domain.Aggregates.VariantAggregate.Events;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using ecommerce.Domain.Common.Models; 

namespace ecommerce.Domain.Aggregates.VariantAggregate;
public sealed class VariantAggregate : AggregateRoot<VariantId, Guid>
{
    public CategoryId CategoryId { get; private set; }
    public string Name { get; private set; }

    private readonly List<VariantOptionEntity> options;
    public IReadOnlyCollection<VariantOptionEntity> Options => options.AsReadOnly();

    private VariantAggregate() { }
    internal VariantAggregate(VariantId id,
                              CategoryId categoryId,
                              string name,
                              List<VariantOptionEntity> attributeValues) : base(id)
    {
        CategoryId = categoryId;
        Name = name;
        options = attributeValues;
    }

    public VariantAggregate AddOption(VariantOptionEntity variationOption)
    {
        options.Add(variationOption);
        return this;
    }

    public VariantAggregate AddOption(IEnumerable<VariantOptionEntity> variationOptions)
    {
        options.AddRange(variationOptions);
        return this;
    }

    public override void Delete()
    {
        RaiseDomainEvent(new VariantDeletedDomainEvent(this));
    }
}