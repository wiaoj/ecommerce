using MediatR;

namespace ecommerce.Application.Features.Categories.Queries.CategoryById;
public sealed record CategoryByIdQuery(Guid Id) : IRequest<CategoryByIdResult>;