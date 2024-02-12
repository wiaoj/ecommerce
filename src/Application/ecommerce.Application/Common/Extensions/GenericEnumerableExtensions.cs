using ecommerce.Domain.Extensions;

namespace ecommerce.Application.Common.Extensions;
public static class GenericEnumerableExtensions {
    public static Boolean CountIsNotZero<T>(this IEnumerable<T> values) {
        return values.Count().IsNotZero();
    }

    public static Boolean CountIsZero<T>(this IEnumerable<T> values) {
        return values.Count().IsZero();
    }
}