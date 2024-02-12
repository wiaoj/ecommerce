using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Common.Models;
using ecommerce.Domain.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate;
public sealed class CategoryAggregate : AggregateRoot<CategoryId, Guid> {
    private readonly List<CategoryId> subcategoryIds;
    private readonly List<ProductId> productIds;
    public CategoryName Name { get; private set; }
    public CategoryId? ParentId { get; private set; }
    public IReadOnlyCollection<CategoryId> SubcategoryIds => this.subcategoryIds.AsReadOnly();
    public IReadOnlyCollection<ProductId> ProductIds => this.productIds.AsReadOnly();

    private CategoryAggregate() { }

    internal CategoryAggregate(CategoryId? parentId,
                               CategoryId categoryId,
                               CategoryName name,
                               List<CategoryId> subcategoryIds) : base(categoryId) {
        this.ParentId = parentId;
        this.Name = name;
        this.subcategoryIds = subcategoryIds;
        this.productIds ??= [];
    }
    public CategoryAggregate SetParentCategory(CategoryId parentCategoryId) {
        ArgumentNullException.ThrowIfNull(parentCategoryId);

        if(this.Id == parentCategoryId) {
            throw new InvalidOperationException("A category cannot be its own parent.");
        }

        if(this.ParentId != parentCategoryId) {
            if(this.ParentId.NotNull())
                RaiseDomainEvent(new ParentCategoryRemovedDomainEvent(this.ParentId, this.Id));

            this.ParentId = parentCategoryId;
            RaiseDomainEvent(new ParentCategoryChangedDomainEvent(this.ParentId, this.Id));
        }

        return this;
    }

    public CategoryAggregate RemoveParentCategory() {
        if(this.ParentId.NotNull()) {
            RaiseDomainEvent(new ParentCategoryRemovedDomainEvent(this.ParentId, this.Id));
            this.ParentId = null;
        }

        return this;
    }

    public CategoryAggregate AddChildCategory(CategoryId categoryId) {
        ArgumentNullException.ThrowIfNull(categoryId);

        if(this.Id == categoryId)
            throw new InvalidOperationException("Invalid child category.");

        if(this.subcategoryIds.Contains(categoryId))
            throw new Exception("Category already exists");

        this.subcategoryIds.Add(categoryId);
        return this;
    }

    public CategoryAggregate AddSubCategory(IEnumerable<CategoryId> categoryIds) {
        foreach(CategoryId categoryId in categoryIds)
            AddChildCategory(categoryId);

        return this;
    }

    public CategoryAggregate RemoveSubCategory(CategoryId categoryId) {
        if(this.subcategoryIds.Remove(categoryId).IsFalse()) 
            throw new InvalidOperationException("Subcategory with ID {0} could not be removed because it does not exist.".Format(categoryId));
        
        return this;
    }

    public CategoryAggregate RemoveSubCategory(IEnumerable<CategoryId> categoryIds) {
        foreach(CategoryId categoryId in categoryIds)
            RemoveSubCategory(categoryId);

        return this;
    }

    public CategoryAggregate AddProduct(ProductId productId) {
        if(this.productIds.Contains(productId))
            throw new ProductAlreadyExistsException(productId);

        this.productIds.Add(productId);
        return this;
    }

    public CategoryAggregate RemoveProduct(ProductId productId) {
        if(this.productIds.Contains(productId).IsFalse())
            throw new ProductNotFoundException(productId);

        this.productIds.Remove(productId);
        return this;
    }

    public override void Delete() {
        RaiseDomainEvent(new CategoryDeletedDomainEvent(this));
    }
}