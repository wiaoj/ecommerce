namespace ecommerce.Application.Features.Categories;
internal partial struct Constants {
    public struct Categories {
        public const String CacheKeyRegistry = "Categories";

        public struct Permissions {
            public const String Create = "create:category";
            public const String Read = "read:category";
            public const String Update = "update:category";
            public const String Delete = "delete:category";
        }
    }
}