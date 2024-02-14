using ecommerce.Application.Features.Categories.Commands.CreateCategory; 
using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Application.Features.Categories.MappingExtensions;
internal static class CreateCategoryExtension {
    public static CreateCategoryCommandResult ToCreateCommandResult(this CategoryAggregate category) {
        return new(category.Id,
                   category.ParentId,
                   category.Name.Value);
    }

    public static CategoryAggregate ToFromCreateCommand(this CreateCategoryCommand createCategoryCommand, ICategoryFactory categoryFactory) {
        return categoryFactory.Create(createCategoryCommand.ParentCategoryId, createCategoryCommand.Name);
    }
}