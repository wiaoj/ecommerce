using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Exceptions;
public sealed class ProductNotFoundException(ProductId productId)
    : NotFoundException("Product not found: {0}".Format(productId.Value));