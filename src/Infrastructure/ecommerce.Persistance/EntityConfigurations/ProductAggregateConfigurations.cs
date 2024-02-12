using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistance.EntityConfigurations;
internal sealed class ProductAggregateConfigurations : IEntityTypeConfiguration<ProductAggregate> {
    public void Configure(EntityTypeBuilder<ProductAggregate> builder) {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

        builder.Property(product => product.CategoryId)
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value));

        builder.Property(productVariant => productVariant.Name)
                .HasConversion(name => name.Value,
                               value => ProductName.Create(value));

        builder.Property(productVariant => productVariant.Description)
            .HasConversion(name => name.Value,
                           value => ProductDescription.Create(value));

        ConfigureProductItemsTable(builder);
    }

    private static void ConfigureProductItemsTable(EntityTypeBuilder<ProductAggregate> builder) {
        builder.OwnsMany(product => product.Variants, navigationBuilder => {
            navigationBuilder.ToTable("ProductVariants");
            navigationBuilder.WithOwner().HasForeignKey(nameof(ProductVariantEntity.ProductId));
            navigationBuilder.HasKey(x => x.Id);
            navigationBuilder.Property(productItem => productItem.Id)
                //.HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                               value => ProductVariantId.Create(value));

            navigationBuilder.Property(productVariant => productVariant.ProductId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                               value => ProductId.Create(value));


            navigationBuilder.Property(productVariant => productVariant.Price)
                .HasConversion(name => name.Value,
                               value => ProductVariantPrice.Create(value));

            ConfigureVariantionOptionTable(navigationBuilder);
        });

        builder.Metadata.FindNavigation(nameof(ProductAggregate.Variants))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureVariantionOptionTable(OwnedNavigationBuilder<ProductAggregate, ProductVariantEntity> navigationBuilder) {
        navigationBuilder.OwnsMany(productItem => productItem.OptionIds, ownedNavigationBuilder => {
            ownedNavigationBuilder.ToTable("ProductVariantOptions");
            ownedNavigationBuilder.WithOwner().HasForeignKey("ProductVariantId");

            ownedNavigationBuilder.Property<Guid>("VariantOptionId");
            ownedNavigationBuilder.HasKey("ProductVariantId", "VariantOptionId");
        });
        //builder.Metadata.FindNavigation(nameof(ProductItemEntity.OptionIds))!
        //.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}