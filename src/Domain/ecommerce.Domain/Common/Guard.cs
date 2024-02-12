using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Domain.Common;
internal static class Guard {
    public static String IfNullOrWhiteSpaceThrow([NotNull] String value) {
        return String.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }
}