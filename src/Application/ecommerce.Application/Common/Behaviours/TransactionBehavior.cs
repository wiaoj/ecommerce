using ecommerce.Application.Common.Interfaces;
using MediatR;
using System.Transactions;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IHasTransaction {
    private readonly IUnitOfWork unitOfWork;

    public TransactionBehavior(IUnitOfWork dataSource) {
        this.unitOfWork = dataSource;
    }

    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken) {
        // The execution is wrapped in a transaction scope to ensure that if any other
        // SaveChanges calls to the data source (e.g. EF Core) are called, that they are
        // transacted atomically. The isolation is set to ReadCommitted by default (i.e. read-
        // locks are released, while write-locks are maintained for the duration of the
        // transaction). Learn more on this approach for EF Core:
        // https://docs.microsoft.com/en-us/ef/core/saving/transactions#using-systemtransactions

        TResponse? response = await next();

        TransactionOptions transactionOptions = new() {
            IsolationLevel = IsolationLevel.ReadCommitted,
        };

        await this.unitOfWork.CreateExecutionStrategyAsync(async (_) => {
            using TransactionScope transaction = new(TransactionScopeOption.Required,
                                                     transactionOptions,
                                                     TransactionScopeAsyncFlowOption.Enabled);

            // By calling SaveChanges at the last point in the transaction ensures that write-
            // locks in the database are created and then released as quickly as possible. This
            // helps optimize the application to handle a higher degree of concurrency.
            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            // Commit transaction if everything succeeds, transaction will auto-rollback when
            // disposed if anything failed.
            transaction.Complete();
        }, cancellationToken);


        return response;
    }
}