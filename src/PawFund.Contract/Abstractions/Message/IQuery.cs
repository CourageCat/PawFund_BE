using PawFund.Contract.Shared;
using MediatR;

namespace PawFund.Contract.Abstractions.Message;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}