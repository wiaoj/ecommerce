using ecommerce.Application.Features.Categories.Commands.CreateCategory; 
using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Application.Features.Categories.MappingExtensions;
public static class CreateCategoryExtension {
    public static CreateCategoryCommandResult ToCreateCommandResult(this CategoryAggregate category) {
        return new(category.Id,
                   category.ParentId,
                   category.Name.Value);
    }

    public static CategoryAggregate FromCreateCommand(this ICategoryFactory categoryFactory, CreateCategoryCommand command) {
        return categoryFactory.Create(command.ParentCategoryId, command.Name);
    }
}