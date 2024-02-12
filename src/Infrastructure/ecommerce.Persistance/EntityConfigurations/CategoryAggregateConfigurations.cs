using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistance.EntityConfigurations;
internal sealed class CategoryAggregateConfigurations : IEntityTypeConfiguration<CategoryAggregate> {
    public void Configure(EntityTypeBuilder<CategoryAggregate> builder) {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value));

        builder.Property(category => category.Name)
            .HasMaxLength(100)
            .HasConversion(
                name => name.Value,
                value => CategoryName.Create(value));

        builder.HasIndex(category => category.Name)
            .IsUnique(true);

        builder.Property(category => category.ParentId)
           .IsRequired(false)
           .ValueGeneratedNever()
           .HasConversion(
               id => id!.Value,
               value => CategoryId.Create(value));

        ConfigureChildCategoryIdsTable(builder);
        ConfigureCategoryProductIdsTable(builder);
    }

    private static void ConfigureCategoryProductIdsTable(EntityTypeBuilder<CategoryAggregate> builder) {
        builder.OwnsMany(category => category.ProductIds, navigationBuilder => {
            navigationBuilder.ToTable("CategoryProductIds");

            navigationBuilder.WithOwner().HasForeignKey("CategoryId");

            navigationBuilder.HasKey("Id");

            navigationBuilder.Property(productId => productId.Value)
                .HasColumnName("ProductId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(CategoryAggregate.ProductIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureChildCategoryIdsTable(EntityTypeBuilder<CategoryAggregate> builder) {
        builder.OwnsMany(category => category.SubcategoryIds, navigationBuilder => {
            navigationBuilder.ToTable("CategoryChildIds");

            navigationBuilder.WithOwner().HasForeignKey("CategoryId");

            navigationBuilder.HasKey("Id");

            navigationBuilder.Property(categoryId => categoryId.Value)
                .HasColumnName("ChildId")
                .ValueGeneratedNever();
        });

        builder.Metadata
            .FindNavigation(nameof(CategoryAggregate.SubcategoryIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}