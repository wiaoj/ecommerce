using System.Text.RegularExpressions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
public partial class CategoryConstants {
    public static partial class Regexes {
        [GeneratedRegex("^[A-Za-zçÇğĞıİöÖşŞüÜ ]+$")]
        public static partial Regex CategoryNameRegex();
    }
}