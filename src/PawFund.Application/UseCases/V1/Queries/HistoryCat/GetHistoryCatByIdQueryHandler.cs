using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.HistoryCats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.HistoryCat;
public sealed class GetHistoryCatByIdQueryHandler : IQueryHandler<Query.GetHistoryCatByIdQuery, Response.HistoryCatResponse>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public GetHistoryCatByIdQueryHandler(IDPUnitOfWork dpUnitOfWork)
    {
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result<Response.HistoryCatResponse>> Handle(Query.GetHistoryCatByIdQuery request, CancellationToken cancellationToken)
    {
        var historycatFound = await _dpUnitOfWork.HistoryCatRepositories.GetByIdAsync(request.Id);
        if (historycatFound == null || historycatFound.IsDeleted == true)
        {
            throw new HistoryCatException.HistoryCatNotFoundException(request.Id);
        }
        var result = new Response.HistoryCatResponse(historycatFound.Id, historycatFound.DateAdopt, historycatFound.CatId, historycatFound.AccountId);
        return Result.Success(result);
    }
}

