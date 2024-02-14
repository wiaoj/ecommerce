using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
internal static partial class CategoryConstants {
    public static class Exceptions {
        public const String CategoryCannotBeOwnParent = "A category cannot be its own parent";
        public const String ParentCategoryAlreadySet = "The specified parent category ID '{0}' is already set as the parent of this category";
        public const String ParentCategoryNotSet = "Cannot remove parent category because it is not set";
        public const String SelfReferencingCategory = "A category cannot be added as a child to itself";
        public const String SubcategoryAlreadyExists = "This subcategory is already added to the category";
        public const String SubcategoryNotFound = "Subcategory with ID {0} could not be removed because it does not exist";
        public const String ProductAlreadyInCategory = "Product with ID {productId} is already added to the category";
        public const String ProductNotInCategory = "Product with ID {0} does not exist in the category";
    }
}