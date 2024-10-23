using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.CatDTOs;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Queries.Cat;

public sealed class GetCatsQueryHandler : IQueryHandler<Query.GetCats, Success<PagedResult<CatDto>>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IMapper _mapper;

    public GetCatsQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Success<PagedResult<CatDto>>>> Handle(Query.GetCats request, CancellationToken cancellationToken)
    {
        var result = await _dpUnitOfWork.CatRepositories.GetAllCatsForAdoptionAsync
            (request.PageIndex,
            request.PageSize,
            request.FilterParams,
            request.SelectedColumns);

        var catDtos = _mapper.Map<List<CatDto>>(result.Items);

        return Result.Success(new Success<PagedResult<CatDto>>("", "", new PagedResult<CatDto>(catDtos, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)));
    }
}
