using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects; 
using ecommerce.Persistance.Context;

namespace ecommerce.Persistance.Repositories;
internal sealed class ProductRepository : Repository<ProductAggregate, ProductId, Guid>, IProductRepository {
    public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
}