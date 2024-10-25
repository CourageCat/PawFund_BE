using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.DonateDTOs;
using PawFund.Contract.Services.Donate;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Queries.Donate;

public sealed class GetDonatesQueryHandler : IQueryHandler<Query.GetDonatesQuery, Success<PagedResult<DonateDto>>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IMapper _mapper;

    public GetDonatesQueryHandler
        (IDPUnitOfWork dpUnitOfWork,
        IMapper mapper)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Success<PagedResult<DonateDto>>>> Handle(Query.GetDonatesQuery request, CancellationToken cancellationToken)
    {
        var result = await _dpUnitOfWork.DonationRepository.GetPagedDonatesAsync(request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);

        var donateDtos = _mapper.Map<List<DonateDto>>(result.Items);

        return Result.Success(new Success<PagedResult<DonateDto>>("", "", new PagedResult<DonateDto>(donateDtos, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)));
    }
}
