using ecommerce.Domain.Common.Models;
using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Domain.Extensions;
public static class StringExtensions {
    public static String Format(this String value, Object? arg) {
        return String.Format(value, arg);
    }

    public static String Format(this String value, params Object[] args) {
        return String.Format(value, args);
    }

    public static Boolean IsNotNullOrEmpty([NotNullWhen(true)] this String? value) {
        return !String.IsNullOrEmpty(value);
    }

    public static Boolean IsNullOrEmpty([NotNullWhen(false)] this String? value) {
        return String.IsNullOrEmpty(value);
    }

    public static Boolean IsNullOrWhiteSpaces([NotNullWhen(false)] this String? value) {
        return String.IsNullOrWhiteSpace(value);
    }
}