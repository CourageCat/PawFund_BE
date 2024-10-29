using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.DonateDTOs;
using PawFund.Contract.Services.Donate;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Queries.Donate;

public sealed class GetDonateByOrderIdQueryHandler : IQueryHandler<Query.GetDonateByOrderIdQuery, Success<DonateDto>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IMapper _mapper;

    public GetDonateByOrderIdQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Success<DonateDto>>> Handle(Query.GetDonateByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _dpUnitOfWork.DonationRepository.GetDonationByOrderIdAsync(request.OrderId);
        var donateDto = _mapper.Map<DonateDto>(result);
        return Result.Success(new Success<DonateDto>("", "", donateDto));
    }
}
