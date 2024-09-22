using PawFund.Contract.Shared;
using MediatR;

namespace PawFund.Contract.Abstractions.Message;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}