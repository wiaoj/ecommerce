using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Domain.Extensions;
public static class BooleanExtensions {
    public static Boolean IsFalse(this Boolean value) {
        return !value;
    }
    public static async Task<Boolean> IsFalse(this Task<Boolean> value) {
        return !await value;
    }

    public static Boolean NotNull<T>([NotNullWhen(true)] this T? value) {
        return value is not null;
    }  
    
    public static Boolean IsNull<T>([NotNullWhen(false)] this T? value) {
        return value is null;
    }
    
    public static Boolean IsNot<T>(this T left, T right) {
        return ReferenceEquals(left, right).IsFalse();
    }
}