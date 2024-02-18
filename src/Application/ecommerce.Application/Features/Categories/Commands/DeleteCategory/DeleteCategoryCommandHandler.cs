using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.DeleteCategory;
internal sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand> {
    private readonly ICategoryRepository categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
        CategoryId id = CategoryId.Create(request.Id);
        CategoryAggregate? category = await this.categoryRepository.FindByIdAsync(id, cancellationToken);

        if(category.IsNull()) 
            throw new CategoryNotFoundException(request.Id);
        
        category.Delete();
        await this.categoryRepository.DeleteAsync(category, cancellationToken);
    }
}