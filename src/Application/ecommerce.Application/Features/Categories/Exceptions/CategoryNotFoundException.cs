using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Application.Features.Categories.Exceptions;
public sealed class CategoryNotFoundException : NotFoundException {
    public CategoryNotFoundException(CategoryId id) : base(CategoryApplicationConstants.ExceptionMessages.NotFound.Format(id)) { }
    public CategoryNotFoundException(Guid id) : base(CategoryApplicationConstants.ExceptionMessages.NotFound.Format(id)) { }
}