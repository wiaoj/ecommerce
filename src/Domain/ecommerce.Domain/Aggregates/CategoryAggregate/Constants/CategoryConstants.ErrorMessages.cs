namespace ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
public partial class CategoryConstants {
    public struct ErrorMessages {
        public const String CategoryCannotBeOwnParent = "A category cannot be its own parent.";
        public const String ParentCategoryAlreadySet = "The parent category with ID '{0}' is already set for this category.";
        public const String ParentCategoryNotSet = "The parent category cannot be removed because it has not been set.";
        public const String SelfReferencingCategory = "A category cannot be added as a subcategory to itself.";
        public const String SubcategoryAlreadyExists = "This subcategory is already included in the category.";
        public const String SubcategoryNotFound = "The subcategory with ID '{0}' could not be removed because it was not found.";
        public const String ProductAlreadyInCategory = "The product with ID '{0}' is already included in the category.";
        public const String ProductNotInCategory = "The product with ID '{0}' was not found in the category.";
        public const String InvalidCategoryName = "The category name '{0}' is invalid.";
    }
}