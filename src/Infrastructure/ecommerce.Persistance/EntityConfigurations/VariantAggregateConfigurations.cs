using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate;
using ecommerce.Domain.Aggregates.VariantAggregate.Entities;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistance.EntityConfigurations;
internal sealed class VariantAggregateConfigurations : IEntityTypeConfiguration<VariantAggregate> {
    public void Configure(EntityTypeBuilder<VariantAggregate> builder) {
        builder.ToTable("Variants");

        builder.HasKey(variation => variation.Id);

        builder.Property(variation => variation.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => VariantId.Create(value));

        builder.Property(variation => variation.Name)
            .IsRequired();

        builder.Property(variation => variation.CategoryId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => CategoryId.Create(value));


        ConfigureVariationOptionEntitityTable(builder);
    }

    private static void ConfigureVariationOptionEntitityTable(EntityTypeBuilder<VariantAggregate> builder) {
        builder.OwnsMany(variant => variant.Options, navigationBuilder => {
            navigationBuilder.ToTable("VariantsOptions");
            navigationBuilder.WithOwner().HasForeignKey(nameof(VariantOptionEntity.VariantId));
            navigationBuilder.HasKey(value => value.Id);

            navigationBuilder.Property(value => value.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => VariantOptionId.Create(value));

            navigationBuilder.Property(value => value.VariantId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => VariantId.Create(value));

            navigationBuilder.Property(value => value.Value)
                .IsRequired()
                .HasConversion(value => value.Value, value => VariantOptionValue.Create(value));
        });

        //builder.Navigation(aggreagate => aggreagate.Values).Metadata.SetField("_values");
        //builder.Navigation(aggreagate => aggreagate.Values).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(VariantAggregate.Options))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}