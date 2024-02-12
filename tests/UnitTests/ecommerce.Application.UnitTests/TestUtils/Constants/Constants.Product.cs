using ecommerce.Application.UnitTests.TestUtils.Extensions;

namespace ecommerce.Application.UnitTests.TestUtils.Constants;
public static partial class Constants {
    public static class Product {
        public static readonly Guid Id = "4aab281e-b52a-40cb-b92f-90020d2c431f".ToGuid();
        public static readonly Guid CategoryId = "4ffda8f9-2113-470c-92d4-1a245bfc0083".ToGuid();
        public const String Name = "Product Name 1"; 
        public const String VariantName = "Product Name 1"; 
        public const String Description = "Product Description";
        public const Decimal Price = 130M;
        public const Int32 Stock = 124;

        public static readonly List<Guid> OptionIds = [Guid.NewGuid(), Guid.NewGuid()];
    }
}