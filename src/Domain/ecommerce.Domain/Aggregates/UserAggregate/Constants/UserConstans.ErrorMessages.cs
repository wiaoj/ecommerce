namespace ecommerce.Domain.Aggregates.UserAggregate.Constants;
internal static partial class UserConstants {
    public struct ErrorMessages {
        public const String PhoneNumberInvalidFormat
            = "The phone number '{0}' is invalid. Please ensure it contains only numbers and matches the expected format";
        public const String InvalidEmailFormat
            = "The email address '{0}' is invalid. Please check for any typos and ensure it follows the format: user@example.com";
        public const String InvalidFirstNameCharacters
            = "The first name '{0}' is invalid. It should only contain alphabetical characters and be 2-30 characters long";
        public const String InvalidLastNameCharacters
            = "The last name '{0}' is invalid. It should only contain alphabetical characters and be 2-30 characters long";
        public const String EmptyPasswordHash = "The password hash cannot be empty. Ensure a valid hash is generated.";
        public const String EmptyPasswordSalt = "The salt cannot be empty. Ensure a valid salt is generated";
        public const String PhoneNumberNotRegistered = "The provided phone number is not registered. Please use a registered phone number";
        public const String RefreshTokenExpired = "The token's created date '{0}' cannot be after its expiration date '{1}'";
    }
}