namespace ecommerce.Domain.Aggregates.ProductAggregate.Constants;
public partial class ProductConstants {
    public struct ErrorMessages {
        public const String InvalidProductDescription = "The category name '{0}' is invalid.";
        public const string ProductDescriptionLengthOutOfRange = "Product description must be between {0} and {1} characters. Please adjust the length of your description.";
        public const string ProductDescriptionContainsInvalidCharacters = "Description can only contain letters and digits.";
    }
}