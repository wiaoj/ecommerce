using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.MappingExtensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using MediatR;

//[assembly: InternalsVisibleTo("ecommerce.Application.UnitTests")]
namespace ecommerce.Application.Features.Categories.Commands.CreateCategory;
internal sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResult> {
    private readonly ICategoryFactory categoryFactory;
    private readonly ICategoryRepository categoryRepository;

    public CreateCategoryCommandHandler(ICategoryFactory categoryFactory, ICategoryRepository categoryRepository) {
        this.categoryFactory = categoryFactory;
        this.categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
        CategoryAggregate category = this.categoryFactory.FromCreateCommand(request);
        await this.categoryRepository.CreateAsync(category, cancellationToken);
        return category.ToCreateCommandResult();
    }
}