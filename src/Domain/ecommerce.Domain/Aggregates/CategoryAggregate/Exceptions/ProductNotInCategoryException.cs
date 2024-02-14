using ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
public class ProductNotInCategoryException(ProductId productId) 
    : DomainValidationException(CategoryConstants.Exceptions.ProductNotInCategory.Format(productId));