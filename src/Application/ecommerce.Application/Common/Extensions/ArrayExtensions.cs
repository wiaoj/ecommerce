using ecommerce.Domain.Extensions;

namespace ecommerce.Application.Common.Extensions;
public static class ArrayExtensions {
    public static Boolean CountIsNotZero<T>(this T[] values) {
        return values.Length.IsNotZero();
    }

    public static Boolean CountIsZero<T>(this T[] values) {
        return values.Length.IsZero();
    }
}