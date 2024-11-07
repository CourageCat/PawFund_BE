using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Account;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.DTOs.EventDTOs.Respone;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using static PawFund.Contract.Services.Event.Respone;
using static PawFund.Contract.Services.EventActivity.Respone;
using static PawFund.Domain.Exceptions.EventException;

namespace PawFund.Application.UseCases.V1.Queries.Event
{
    public sealed class GetAllEventQueryHandler : IQueryHandler<Query.GetAllEvent, Success<PagedResult<EventForUserDTO.EventDTO>>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public GetAllEventQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper, IEventRepository eventRepository)
        {
            _dpUnitOfWork = dpUnitOfWork;
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<Result<Success<PagedResult<EventForUserDTO.EventDTO>>>> Handle(Query.GetAllEvent request, CancellationToken cancellationToken)
        {
            var result = await _eventRepository.GetAllEventAsync(request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);

            if(result.Items.Count == 0)
            {
                throw new EventNotFoundByAdminException();
            }

            var eventDtos = _mapper.Map<List<EventForUserDTO.EventDTO>>(result.Items);

            return Result.Success(new Success<PagedResult<EventForUserDTO.EventDTO>>(MessagesList.GetEventsSuccess.GetMessage().Code, MessagesList.GetEventsSuccess.GetMessage().Message, new PagedResult<EventForUserDTO.EventDTO>(eventDtos, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)));
        }
    }
}
    