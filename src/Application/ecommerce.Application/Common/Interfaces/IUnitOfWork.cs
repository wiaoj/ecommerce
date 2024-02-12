namespace ecommerce.Application.Common.Interfaces;
public interface IUnitOfWork {
    Task CreateExecutionStrategyAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    //Task BeginTransactionAsync(CancellationToken cancellationToken);
    //Task CommitTransactionAsync(CancellationToken cancellationToken);
    //Task RollbackTransactionAsync(CancellationToken cancellationToken);
}