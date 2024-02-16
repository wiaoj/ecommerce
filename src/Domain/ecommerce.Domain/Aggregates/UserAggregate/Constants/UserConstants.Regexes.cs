using System.Text.RegularExpressions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Constants;
internal static partial class UserConstants {
    public static partial class Regexes {

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public static partial Regex EmailRegex();
        [GeneratedRegex(@"^[0-9]+$")]
        public static partial Regex PhoneNumberRegex();

        [GeneratedRegex(@"^[A-Za-z]{2,30}$")]
        public static partial Regex FirstNameRegex();
        [GeneratedRegex(@"^[A-Za-z]{2,30}$")]
        public static partial Regex LastNameRegex();
    }
}