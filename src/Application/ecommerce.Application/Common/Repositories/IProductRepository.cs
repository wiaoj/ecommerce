using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;

namespace ecommerce.Application.Common.Repositories;
public interface IProductRepository : IRepository<ProductAggregate, ProductId, Guid> { }