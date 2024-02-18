using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Application.Features.Categories.MappingExtensions;
internal static class CreateCategoryExtensions {
    public static CategoryAggregate FromCreateCommand(this ICategoryFactory categoryFactory, CreateCategoryCommand command) {
        CategoryId? parentId = categoryFactory.CreateId(command.ParentCategoryId);
        CategoryName categoryName = categoryFactory.CreateCategoryName(command.Name);
        return categoryFactory.Create(parentId, categoryName);
    }

    public static CreateCategoryCommandResult ToCreateCategoryCommandResult(this CategoryAggregate category) {
        return new(category.Id.Value);
    }
}