﻿using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.DeleteCategory;
internal sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand> {
    private readonly ICategoryRepository categoryRepository;
    private readonly IGuardClause guardClause;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IGuardClause guardClause) {
        this.categoryRepository = categoryRepository;
        this.guardClause = guardClause;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
        CategoryId id = CategoryId.Create(request.Id);
        CategoryAggregate? category = await this.categoryRepository.FindByIdAsync(id, cancellationToken);
        this.guardClause.ThrowIfNull(category, new CategoryNotFoundException(request.Id));
        category.Delete();
        await this.categoryRepository.DeleteAsync(category, cancellationToken);
    }
}