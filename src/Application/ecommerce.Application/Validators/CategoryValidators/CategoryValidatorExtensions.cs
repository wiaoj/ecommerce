﻿using ecommerce.Application.Common.Constants;
using ecommerce.Application.Common.Extensions;
using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using FluentValidation;

namespace ecommerce.Application.Validators.CategoryValidators;
internal static class CategoryValidatorExtensions {
    public static IRuleBuilderOptions<T, Guid> CategoryExist<T>(this IRuleBuilder<T, Guid> ruleBuilder,
                                                                      ICategoryRepository categoryRepository,
                                                                      ICategoryFactory categoryFactory) {
        async Task<Boolean> predicate(Guid id, CancellationToken cancellationToken) {
            return await categoryRepository.ExistsAsync(categoryFactory.CreateCategoryId(id)!, cancellationToken);
        }
        return ruleBuilder.MustAsync(predicate).WithMessage(Errors.Category.NotFound);
    }

    public static IRuleBuilderOptions<T, String> CategoryExist<T>(this IRuleBuilder<T, String> ruleBuilder,
                                                                      ICategoryRepository categoryRepository,
                                                                      ICategoryFactory categoryFactory) {
        async Task<Boolean> predicate(String name, CancellationToken cancellationToken) {
            return await categoryRepository.ExistsByNameAsync(categoryFactory.CreateCategoryName(name), cancellationToken).IsFalse();
        }
        return ruleBuilder.MustAsync(predicate).WithMessage(Errors.Category.NotFound);
    }

    public static IRuleBuilderOptions<T, Guid?> CategoryExistIfIdNotNull<T>(this IRuleBuilder<T, Guid?> ruleBuilder,
                                                                      ICategoryRepository categoryRepository,
                                                                      ICategoryFactory categoryFactory) {
        if(ruleBuilder.Must(x => x.HasValue) is null)
            return ruleBuilder.Must(x => false);

        async Task<Boolean> predicate(Guid? id, CancellationToken cancellationToken) {
            return await categoryRepository.ExistsAsync(categoryFactory.CreateCategoryId(id)!, cancellationToken);
        }
        return ruleBuilder.MustAsync(predicate).WithMessage(Errors.Category.NotFound);
    }
}