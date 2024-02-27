using System.Text.RegularExpressions;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Constants;
public partial class ProductConstants {
    public partial struct Regexes {
        [GeneratedRegex(@"^(?:[a-zA-ZçÇğĞıİöÖşŞüÜ0-9-_ ]{1,64})$")]
        public static partial Regex ProductNameRegex();
        [GeneratedRegex(@"^")]
        public static partial Regex ProductDescriptionRegex();
    }
}