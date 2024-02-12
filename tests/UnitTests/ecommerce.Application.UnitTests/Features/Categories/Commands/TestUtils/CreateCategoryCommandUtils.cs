using ecommerce.Application.Features.Categories.Commands.CreateCategory;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.TestUtils;
public static class CreateCategoryCommandUtils
{
    public static CreateCategoryCommand CreateCommand()
    {
        return new(Constants.Common.XRequestId, null, Category.Name);
    }

    public static CreateCategoryCommand CreateCommandWithParent()
    {
        return new(Constants.Common.XRequestId, Category.ParentId, Category.Name);
    }
}