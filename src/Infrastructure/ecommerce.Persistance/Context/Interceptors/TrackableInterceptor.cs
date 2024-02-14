using ecommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ecommerce.Persistance.Context.Interceptors;
internal sealed class TrackableInterceptor(IDateTimeProvider dateTimeProvider) : SaveChangesInterceptor {
    public override ValueTask<InterceptionResult<Int32>> SavingChangesAsync(DbContextEventData eventData,
                                                                            InterceptionResult<Int32> result,
                                                                            CancellationToken cancellationToken = default) {
        DbContext? context = eventData.Context;

        if(context is null)
            return ValueTask.FromResult(result);
        
        foreach(EntityEntry entry in context.ChangeTracker.Entries()) {
            if(entry is not { State: EntityState.Modified or EntityState.Added, Entity: ITrackable trackableEntity })
                continue;

            trackableEntity.UpdateLastModified(dateTimeProvider);

        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}