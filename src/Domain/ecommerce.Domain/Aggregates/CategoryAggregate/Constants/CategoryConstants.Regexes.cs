using System.Text.RegularExpressions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
public partial class CategoryConstants {
    public static partial class Regexes {
        [GeneratedRegex("^(?:(?!-)(?:[a-zçğıöşü0-9-_]{1,64})(?:-[a-zçğıöşü0-9-_]{1,64})*)*$", RegexOptions.IgnoreCase)]
        public static partial Regex CategoryNameRegex();
    }
}