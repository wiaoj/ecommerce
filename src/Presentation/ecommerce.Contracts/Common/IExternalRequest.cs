using MediatR;

namespace ecommerce.Contracts.Common;
public interface IExternalRequest<out TCommand> where TCommand : MediatR.IBaseRequest {
    TCommand ToCommand() ;
}