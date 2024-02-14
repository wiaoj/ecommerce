using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Persistance.Context;
public interface IFactoryInjector {
    ICategoryFactory CategoryFactory { get; }
}

internal sealed class FactoryInjector : IFactoryInjector {
    public ICategoryFactory CategoryFactory { get; }

    public FactoryInjector(ICategoryFactory categoryFactory) {
        this.CategoryFactory = categoryFactory;
    }
}