namespace ecommerce.Application.Features.Categories;
internal static class CategoryApplicationConstants {
    public const String CacheGroupKeyRegistry = "Categories";

    public static class ExceptionMessages {
        public const String NotFound = "Category with id '{0}' not found";
    }

    public static class Permissions {
        public const String Create = "create:category";
        public const String Read = "read:category";
        public const String Update = "update:category";
        public const String Delete = "delete:category";
    }

    public static class ValidationMessages {
        public static class CategoryName {
            public const String CannotBeEmpty = "Category name cannot be empty";
            public const String Required = "Category name is required";
            public const String MaxLength = "Category name must be {MaxLength} characters or fewer";
        }

        public static class Id {
            public const String CannotBeEmpty = "Category id cannot be empty";
            public const String Required = "Category id is required";
            public const String ParentIdCannotBeEmpty = "Category parent id is required";
        }

        public const String CategoryNotFound = "Category not found.";
        public const String CategoryAlreadyExists = "Category already exists.";
        public const String ParentCategoryDoesNotExist = "Parent category does not exist.";
    }
}