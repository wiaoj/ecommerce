using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Application.Common.Constants;
internal partial record Errors {
    public struct Category {
        public const String NotFound = "Category not found";
        public static String NotFoundById(Guid id) {
            return $"Category with id '{id}' not found";
        }

        public static String NotFoundById(CategoryId id) {
            return $"Category with id '{id.Value}' not found";
        }
    }
}