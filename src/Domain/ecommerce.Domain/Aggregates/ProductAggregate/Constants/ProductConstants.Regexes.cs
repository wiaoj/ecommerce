using System.Text.RegularExpressions;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Constants;
public partial class ProductConstants {
    public partial struct Regexes {
        [GeneratedRegex(@"^[a-zA-Z0-9\s\.\,\-]+$")]
        public static partial Regex ProductDescriptionRegex();
    }
}