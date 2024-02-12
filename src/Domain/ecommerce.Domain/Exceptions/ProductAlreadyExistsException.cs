using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Exceptions;
public sealed class ProductAlreadyExistsException(ProductId productId) 
    : ConflictException("Ürün zaten mevcut: {0}".Format(productId.Value));