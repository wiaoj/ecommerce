namespace ecommerce.Application.UnitTests.TestUtils.Constants;
public static partial class Constants {
    public static class Common {
        public static Guid XRequestId = Guid.NewGuid();
        public static String XRequestIdAsString = XRequestId.ToString();
    }
}