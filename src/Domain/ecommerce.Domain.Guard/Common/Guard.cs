using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Domain.Guard.Common;
internal static class GuardBase {
    public static String IfNullOrWhiteSpaceThrow([NotNull] String value) {
        return String.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }
}