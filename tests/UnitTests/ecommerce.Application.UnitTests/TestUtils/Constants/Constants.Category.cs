using ecommerce.Application.UnitTests.TestUtils.Extensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Application.UnitTests.TestUtils.Constants;
public static partial class Constants {
    public static class Category {
        public static readonly Guid Id = "5cab971e-b52a-40eb-b92f-90f40d2c532f".ToGuid();
        public static readonly Guid ParentId = "4ffda8f9-2113-470c-92d4-1a245bfc0083".ToGuid();
        public static readonly Guid ChildCategoryId = "60537e8a-1404-4135-9ca6-a1d34e5a8671".ToGuid();
        public const String Name = "Category Name";
        public const String ChildName = "Child Category Name";
        public const String Description = "Category Description";

        public static IEnumerable<Guid> ChildCategoryIds(Int32 count) {
            return Enumerable.Range(0, count)
                .Select(x => Guid.NewGuid())
                .ToList();
        }

        private const Int32 MinimumChildCount = 0;
        private const Int32 MaximumChildCount = 50;
        public readonly static Int32 ChildCount = Random.Shared.Next(MinimumChildCount, MaximumChildCount);

        public static CategoryAggregate CreateValidCategory() {
            return CategoryTestFactory.CreateValidCategoryAggregate();
        }
    }
}