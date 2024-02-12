using ecommerce.Application.Common.Extensions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Application.UnitTests.TestUtils.Extensions;
internal static class StringExtensions {
    public static Guid ToGuid(this String value) {
        if(Guid.TryParse(value, out Guid result).IsFalse()) 
            throw new FormatException("The string is not a valid GUID format.");
        return result;
    }
} 