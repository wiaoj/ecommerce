using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Application.Common.Guard;
internal class GuardClause : IGuardClause {
    public void ThrowIfNull<T, TException>([NotNull] T? entity, TException exception) where TException : WApplicationException {
        ThrowIfConditionIsTrue(entity is null, exception);
    }

    public void ThrowIfPredicate<T, TException>([NotNull] T entity,
                                                TException exception,
                                                [DoesNotReturnIf(true)] Func<T, Boolean> predicate)
        where TException : WApplicationException {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(predicate);
        ThrowIfConditionIsTrue(predicate(entity), exception);
    }

    private static void ThrowIfConditionIsTrue<TException>([DoesNotReturnIf(true)] Boolean condition, TException exception)
        where TException : Exception {
        ArgumentNullException.ThrowIfNull(exception);
        if(condition)
            throw exception;
    }

    public void ThrowIfCondition<TException>([DoesNotReturnIf(true)] Boolean condition, TException exception)
        where TException : WApplicationException {
        ThrowIfConditionIsTrue(condition, exception);
    }

    public void ThrowIfNotCondition<TException>([DoesNotReturnIf(false)] Boolean condition, TException exception)
        where TException : WApplicationException {
        ThrowIfConditionIsTrue(condition.IsFalse(), exception);
    }
}