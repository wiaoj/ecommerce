namespace ecommerce.Domain.Extensions;
public static class IntegerExtensions {
    public static Boolean IsNotZero(this Int32 value) {
        return value is not default(Int32);
    }

    public static Boolean IsZero(this Int32 value) {
        return value is default(Int32);
    }
}