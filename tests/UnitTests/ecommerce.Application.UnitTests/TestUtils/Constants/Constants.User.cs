using Bogus;
using ecommerce.Application.UnitTests.TestUtils.Extensions;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Application.UnitTests.TestUtils.Constants;
public partial class Constants {
    public static class User {
        public static readonly Guid Id = "5cab923e-b52a-40eb-b92f-90f40d2c532f".ToGuid();
        public static readonly Guid ParentId = "4ffa28f9-2113-470c-92d4-1a245bfc0083".ToGuid();
        public static readonly Guid ChildCategoryId = "60237e8a-1404-4135-9ca6-a1d34e5a8671".ToGuid();
        public const String FirstName = "Name";
        public const String? MiddleNames = null;
        public const String LastName = "Surname";
        public const String Email = "test@test.com";
        public const String? PhoneNumber = null;
        public const String Password = "password";     }
}