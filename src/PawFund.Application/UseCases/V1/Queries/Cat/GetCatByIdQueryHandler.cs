using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.Cat;
public sealed class GetCatByIdQueryHandler : IQueryHandler<Query.GetCatByIdQuery, Response.CatResponse>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public GetCatByIdQueryHandler(IDPUnitOfWork dpUnitOfWork)
    {
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result<Response.CatResponse>> Handle(Query.GetCatByIdQuery request, CancellationToken cancellationToken)
    {
        var catFound = await _dpUnitOfWork.CatRepositories.GetByIdAsync(request.Id);
        if(catFound == null || catFound.IsDeleted == true)
        {
            throw new CatException.CatNotFoundException(request.Id);
        }
        var result = new Response.CatResponse(catFound.Id, catFound.Sex, catFound.Name, catFound.Age, catFound.Breed, catFound.Size, catFound.Color, catFound.Description);
        return Result.Success(result);
    }
}

