using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.Services.EventActivity;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Contract.Services.EventActivity.Respone;

namespace PawFund.Application.UseCases.V1.Queries.EventActivity;

public sealed class GetApprovedEventsActivityQueryHandler : IQueryHandler<Query.GetApprovedEventsActivityQuery, Success<List<Respone.EventActivityResponse>>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IMapper _mapper;

    public GetApprovedEventsActivityQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<Success<List<EventActivityResponse>>>> Handle(Query.GetApprovedEventsActivityQuery request, CancellationToken cancellationToken)
    {
        var queryActivity = await _dpUnitOfWork.EventActivityRepositories.GetApprovedEventsActivityId(request.EventId);

        var responses = queryActivity.Select(eventActivity =>
        {
            var activityDTO = _mapper.Map<GetEventActivityByIdDTO.ActivityDTO>(eventActivity);
            var eventDTO = _mapper.Map<GetEventActivityByIdDTO.EventDTO>(eventActivity.Event);
            return new EventActivityResponse(activityDTO, eventDTO);
        }).ToList();

        return Result.Success(new Success<List<EventActivityResponse>> ("", "", responses));
    }
}
