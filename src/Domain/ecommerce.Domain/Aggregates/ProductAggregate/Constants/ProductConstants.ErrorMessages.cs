namespace ecommerce.Domain.Aggregates.ProductAggregate.Constants;
public partial class ProductConstants {
    public struct ErrorMessages {
        public const String InvalidProductDescription = "The category name '{0}' is invalid.";
        public const String ProductDescriptionLengthOutOfRange = "Product description must be between {0} and {1} characters. Please adjust the length of your description.";
        public const String ProductDescriptionContainsInvalidCharacters = "Description can only contain letters and digits.";

        public const String InvalidProductName = "The product name '{0}' is invalid.";
        public const String ProductNameLengthOutOfRange = "Product name must be between {0} and {1} characters. Please adjust the length of your name.";
        public const String ProductNameContainsInvalidCharacters = "Product name can only contain letters, digits, spaces, and hyphens.";

    }
}