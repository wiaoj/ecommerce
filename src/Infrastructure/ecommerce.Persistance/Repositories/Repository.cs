using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Common.Models;
using ecommerce.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce.Persistance.Repositories;
internal abstract class Repository<TEntity, TId, TIdType> : IRepository<TEntity, TId, TIdType>
    where TId : AggregateRootId<TIdType>
    where TEntity : AggregateRoot<TId, TIdType> {
    private readonly ApplicationDbContext context;  
    //IApplicationDbContext<TEntity> { query: set<TEntity>(); }
    protected Repository(ApplicationDbContext applicationDbContext) {
        this.context = applicationDbContext;
    }

    protected ApplicationDbContext Context => this.context;
    protected IQueryable<TEntity> Query => this.context.Set<TEntity>();

    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken) {
        return this.context.AddAsync(entity, cancellationToken).AsTask();
    }

    public Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) {
        return this.context.AddRangeAsync(entities, cancellationToken);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken) {
        return Task.Run(() => {
            this.context.Remove(entity);
        }, cancellationToken);
    }

    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) {
        return Task.Run(() => {
            this.context.RemoveRange(entities);
        }, cancellationToken);
    }

    public Task<Boolean> ExistsAsync(TId id, CancellationToken cancellationToken) {
        return this.Query.AsNoTracking().AnyAsync(entity => entity.Id == id, cancellationToken);
    }

    public Task<Boolean> ExistsAsync(Expression<Func<TEntity, Boolean>> expression, CancellationToken cancellationToken) {
        return this.Query.AsNoTracking().AnyAsync(expression, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken) {
        return await this.Query.AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken) {
        return this.context.FindAsync<TEntity>([id], cancellationToken).AsTask();
        //return this.Query.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public Task<TEntity?> FindByIdAsync(TId id, Boolean tracking, CancellationToken cancellationToken) {
        return tracking 
            ? FindByIdAsync(id, cancellationToken)
            : this.Query.SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken) {
        return Task.Run(() => {
            this.context.Update(entity);
        }, cancellationToken);
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) {
        return Task.Run(() => {
            this.context.UpdateRange(entities);
        }, cancellationToken);
    }
}