using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.EventActivity;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Exceptions;
using static PawFund.Contract.DTOs.EventActivity.GetEventActivityByIdDTO;
using static PawFund.Contract.Services.EventActivity.Respone;

namespace PawFund.Application.UseCases.V1.Queries.EventActivity
{
    public sealed class GetEventActivityByIdQueryHandler : IQueryHandler<Query.GetEventActivityByIdQuery, Success<Respone.EventActivityResponse>>
    {
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public GetEventActivityByIdQueryHandler(IDPUnitOfWork dPUnitOfWork)
        {
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result<Success<EventActivityResponse>>> Handle(Query.GetEventActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var existActivity = await _dPUnitOfWork.EventActivityRepositories.GetByIdAsync(request.Id);
            if (existActivity == null)
            {
                throw new EventActivityException.EventActivityNotFoundException(request.Id);
            }
            else
            {
                ActivityDTO activityDTO = new ActivityDTO()
                {
                    Id = request.Id,
                    Description = existActivity.Description,
                    Name = existActivity.Name,
                    Quantity = existActivity.Quantity,
                    StartDate = existActivity.StartDate,
                    Status = existActivity.Status
                };
                GetEventActivityByIdDTO.EventDTO eventDTO = new GetEventActivityByIdDTO.EventDTO()
                {
                    Id = existActivity.Event.Id,
                    Description = existActivity.Event.Description,
                    Name = existActivity.Event.Name,
                    StartDate = existActivity.Event.StartDate,
                    EndDate = existActivity.Event.EndDate,
                    MaxAttendees = existActivity.Event.MaxAttendees,
                    Status = existActivity.Event.Status.ToString()
                };
                var result = new Respone.EventActivityResponse(activityDTO, eventDTO);
                return Result.Success(new Success<Respone.EventActivityResponse>(MessagesList.GetEventActivityByIdSuccess.GetMessage().Code, MessagesList.GetEventActivityByIdSuccess.GetMessage().Message, result));

            }
        }
    }
}
