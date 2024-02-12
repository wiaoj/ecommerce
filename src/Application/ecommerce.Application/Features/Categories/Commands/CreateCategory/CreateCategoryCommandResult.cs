namespace ecommerce.Application.Features.Categories.Commands.CreateCategory;
public sealed record CreateCategoryCommandResult(Guid Id, Guid? ParentId, String Name);