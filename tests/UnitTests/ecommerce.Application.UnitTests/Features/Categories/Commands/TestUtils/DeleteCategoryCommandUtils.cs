using ecommerce.Application.Features.Categories.Commands.DeleteCategory;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.TestUtils;
public static class DeleteCategoryCommandUtils
{
    public static DeleteCategoryCommand CreateCommand()
    {
        return new(Constants.Common.XRequestId, Category.Id);
    }
}