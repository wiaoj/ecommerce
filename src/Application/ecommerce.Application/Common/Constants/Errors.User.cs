using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Application.Common.Constants;
internal partial record Errors {
    public struct User {
        public const String NotFound = "User not found";
        public static String NotFoundById(Guid id) {
            return $"Category with id '{id}' not found";
        } 
    }
}