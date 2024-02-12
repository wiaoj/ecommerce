using ecommerce.Domain.Common.Models;
using System.Linq.Expressions;

namespace ecommerce.Application.Common.Repositories;
public interface IRepository<TEntity, TId, TIdType>
    where TId : AggregateRootId<TIdType>
    where TEntity : AggregateRoot<TId, TIdType> {
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task<Boolean> ExistsAsync(TId id, CancellationToken cancellationToken);
    Task<Boolean> ExistsAsync(Expression<Func<TEntity, Boolean>> expression, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken);
    Task<TEntity?> FindByIdAsync(TId id, Boolean tracking, CancellationToken cancellationToken);
}