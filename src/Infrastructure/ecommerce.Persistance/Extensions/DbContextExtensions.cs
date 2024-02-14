using ecommerce.Persistance.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistance.Extensions;
internal static class DbContextExtensions {
    public static ModelBuilder ApplyEntityConfigurations(this ModelBuilder modelBuilder, Context.IFactoryInjector factoryInjector) {
        return modelBuilder
            .ApplyConfiguration(new CategoryAggregateConfigurations(factoryInjector.CategoryFactory))
            .ApplyConfiguration(new ProductAggregateConfigurations())
            .ApplyConfiguration(new VariantAggregateConfigurations())
            .ApplyConfiguration(new UserAggregateConfigurations());
    }
}