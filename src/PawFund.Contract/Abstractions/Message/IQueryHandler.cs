using PawFund.Contract.Shared;
using MediatR;

namespace PawFund.Contract.Abstractions.Message;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
