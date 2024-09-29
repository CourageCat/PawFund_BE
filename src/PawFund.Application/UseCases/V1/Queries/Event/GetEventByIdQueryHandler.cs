using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Contract.Services.Events;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.Event
{
    public sealed class GetEventByIdQueryHandler : IQueryHandler<Query.GetEventById, Response.EventResponse>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;

        public GetEventByIdQueryHandler(IRepositoryBase<Domain.Entities.Event, Guid> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Result<Response.EventResponse>> Handle(Query.GetEventById request, CancellationToken cancellationToken)
        {
            var existEvent = await _eventRepository.FindByIdAsync(request.Id,default,e => e.Branch);
            if (existEvent != null)
            {
                var result = new Response.EventResponse(existEvent.Name, existEvent.StartDate, existEvent.EndDate, existEvent.Description, existEvent.MaxAttendees, existEvent.Branch);
                return Result.Success(result);
            }
            else
            {
                throw new EventException.EventNotFoundException("Can not find this event");
            }
        }
    }
}
