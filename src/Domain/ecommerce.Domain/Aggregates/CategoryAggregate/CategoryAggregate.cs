using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
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

    /// <summary>
    /// Sets the parent category for the current category.
    /// <list type="table">
    /// Raises; <br />
    /// <see cref="ParentCategoryRemovedDomainEvent"/> when the current category already has a different parent. <br />
    /// <seealso cref="ParentCategoryChangedDomainEvent"/> when a new parent category is set.
    /// </list>
    /// </summary>
    /// <param name="parentCategoryId">The ID of the new parent category.</param>
    /// <exception cref="CategoryCannotBeOwnParentException" />
    /// <exception cref="ParentCategoryAlreadySetException" />
    public void SetParentCategory(CategoryId parentCategoryId) {
        ArgumentNullException.ThrowIfNull(parentCategoryId);

        if(this.Id == parentCategoryId)
            throw new CategoryCannotBeOwnParentException();

        if(this.ParentId == parentCategoryId)
            throw new ParentCategoryAlreadySetException(parentCategoryId);

        if(this.ParentId.NotNull())
            RaiseDomainEvent(new ParentCategoryRemovedDomainEvent(this.ParentId, this.Id));

        this.ParentId = parentCategoryId;
        RaiseDomainEvent(new ParentCategoryChangedDomainEvent(this.ParentId, this.Id));
    }

    /// <summary>
    /// Removes the parent category from the current category.
    /// <list type="table">
    /// Raises; <br />
    /// <see cref="ParentCategoryRemovedDomainEvent"/> when the parent category is successfully removed.
    /// </list>
    /// </summary>
    /// <exception cref="ParentCategoryNotSetException" />
    public void RemoveParentCategory() {
        if(this.ParentId.IsNull())
            throw new ParentCategoryNotSetException();

        RaiseDomainEvent(new ParentCategoryRemovedDomainEvent(this.ParentId, this.Id));
        this.ParentId = null;
    }

    /// <summary>
    /// Adds a child category to the current category.
    /// <list type="table">
    /// Raises;
    /// <see cref="ChildCategoryAddedDomainEvent"/> 
    /// </list>
    /// </summary>
    /// <param name="categoryId">The ID of the child category to add.</param>
    /// <exception cref="SelfReferencingCategoryException" />
    /// <exception cref="SubcategoryAlreadyExistsException" />
    public void AddChildCategory(CategoryId categoryId) {
        ArgumentNullException.ThrowIfNull(categoryId);

        if(this.Id == categoryId)
            throw new SelfReferencingCategoryException();

        if(this.subcategoryIds.Contains(categoryId))
            throw new SubcategoryAlreadyExistsException();

        this.subcategoryIds.Add(categoryId);
        RaiseDomainEvent(new ChildCategoryAddedDomainEvent(this.Id, categoryId));
    }

    /// <summary>
    /// Adds multiple child categories to the current category.
    /// </summary>
    /// <param name="categoryIds">A collection of child category IDs to add.</param>
    /// <exception cref="SelfReferencingCategoryException" /> 
    /// <exception cref="SubcategoryAlreadyExistsException" /> 
    public void AddSubCategory(IEnumerable<CategoryId> categoryIds) {
        ArgumentNullException.ThrowIfNull(categoryIds);

        foreach(CategoryId categoryId in categoryIds)
            AddChildCategory(categoryId);
    }

    /// <summary>
    /// Removes a child category from the current category.
    /// </summary>
    /// <param name="categoryId">The ID of the child category to remove.</param>
    /// <exception cref="SubcategoryNotFoundException" />
    public void RemoveSubCategory(CategoryId categoryId) {
        ArgumentNullException.ThrowIfNull(categoryId);

        if(this.subcategoryIds.Remove(categoryId).IsFalse())
            throw new SubcategoryNotFoundException(categoryId);
    }

    /// <summary>
    /// Removes multiple child categories from the current category.
    /// </summary>
    /// <param name="categoryIds">A collection of child category IDs to remove.</param>
    /// <exception cref="SubcategoryNotFoundException" />
    public void RemoveSubCategory(IEnumerable<CategoryId> categoryIds) {
        ArgumentNullException.ThrowIfNull(categoryIds);

        foreach(CategoryId categoryId in categoryIds)
            RemoveSubCategory(categoryId);
    }

    /// <summary>
    /// Adds a product to the current category.
    /// <list type="table">
    /// Raises; <br />
    /// <see cref="ProductAddedToCategoryDomainEvent"/> when a product is successfully added to the category.
    /// </list>
    /// </summary>
    /// <param name="productId">The ID of the product to add.</param>
    /// <exception cref="ProductAlreadyInCategoryException" />
    public void AddProduct(ProductId productId) {
        ArgumentNullException.ThrowIfNull(productId);
        RemoveSubCategory([]);
        if(this.productIds.Contains(productId))
            throw new ProductAlreadyInCategoryException(productId);

        this.productIds.Add(productId);

        RaiseDomainEvent(new ProductAddedToCategoryDomainEvent(productId, this.Id));
    }

    /// <summary>
    /// Removes a product from the current category.
    /// <list type="table">
    /// Raises; <br />
    /// <see cref="ProductRemovedFromCategoryDomainEvent"/> when a product is successfully removed from the category.
    /// </list>
    /// </summary>
    /// <param name="productId">The ID of the product to remove.</param>
    /// <exception cref="ProductNotInCategoryException">Thrown if the product does not exist in the category.</exception>
    public void RemoveProduct(ProductId productId) {
        ArgumentNullException.ThrowIfNull(productId);

        if(this.productIds.Remove(productId).IsFalse())
            throw new ProductNotInCategoryException(productId);

        RaiseDomainEvent(new ProductRemovedFromCategoryDomainEvent(productId, this.Id));
    }

    /// <summary>
    /// <list type="table">
    /// Raises; <br />
    /// <see cref="CategoryDeletedDomainEvent"/>
    /// </list>
    /// </summary>
    public override void Delete() {
        RaiseDomainEvent(new CategoryDeletedDomainEvent(this));
    }
}