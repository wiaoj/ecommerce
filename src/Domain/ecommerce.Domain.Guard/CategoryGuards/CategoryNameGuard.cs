using ecommerce.Domain.Guard.Common;
using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Domain.Guard.CategoryGuards;
public static class CategoryNameGuard {
    public static String CheckCategoryNameIfNullOrWhiteSpaceThrow([NotNull] String value) {
        return GuardBase.IfNullOrWhiteSpaceThrow(value);
    }
}