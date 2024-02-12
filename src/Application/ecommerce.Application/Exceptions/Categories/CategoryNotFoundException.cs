using ecommerce.Application.Common.Constants;
using ecommerce.Domain.Common.Exceptions;

namespace ecommerce.Application.Exceptions.Categories;
public sealed class CategoryNotFoundException : NotFoundException {
    public CategoryNotFoundException() : base(Errors.Category.NotFound) { }
    public CategoryNotFoundException(Guid id) : base(Errors.Category.NotFoundById(id)) { }
}