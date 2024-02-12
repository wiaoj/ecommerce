using ecommerce.Domain.Common.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Application.Common.Guard;
public interface IGuardClause {
    void ThrowIfCondition<TException>([DoesNotReturnIf(true)] Boolean condition, TException exception)
        where TException : WApplicationException;
    void ThrowIfNotCondition<TException>([DoesNotReturnIf(false)] Boolean condition, TException exception)
        where TException : WApplicationException;
    void ThrowIfNull<T, TException>([NotNull] T? entity, TException exception)
        where TException : WApplicationException;
    void ThrowIfPredicate<T, TException>(
        [NotNull] T entity,
        TException exception,
        [DoesNotReturnIf(true)] Func<T, Boolean> predicate) where TException : WApplicationException;
}