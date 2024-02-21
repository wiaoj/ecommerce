namespace ecommerce.Domain.Aggregates.ProductAggregate.Constants;
public partial class ProductConstants {
    public partial struct Rules {
        public struct Description {
            public const Int32 MinimumLength = 10;
            public const Int32 MaximumLength = 500;
        }
    }
}