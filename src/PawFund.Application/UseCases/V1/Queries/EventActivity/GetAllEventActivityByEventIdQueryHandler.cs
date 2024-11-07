using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Adopt.Response;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.DTOs.EventDTOs.Respone;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.EventActivity;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Contract.DTOs.EventActivity.GetEventActivityByIdDTO;
using static PawFund.Contract.Services.EventActivity.Respone;

namespace PawFund.Application.UseCases.V1.Queries.EventActivity
{
    public sealed class GetAllEventActivityByEventIdQueryHandler : IQueryHandler<Query.GetAllEventActivity, Success<PagedResult<GetEventActivityByIdDTO.ActivityDTO>>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllEventActivityByEventIdQueryHandler(IDPUnitOfWork dpUnitOfWork)
        {
            _dpUnitOfWork = dpUnitOfWork;
        }

        public async Task<Result<Success<PagedResult<GetEventActivityByIdDTO.ActivityDTO>>>> Handle(Query.GetAllEventActivity request, CancellationToken cancellationToken)
        {
            var result = await _dpUnitOfWork.EventActivityRepositories.GetAllByEventId(request.EventId,request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);

            var resultItems = result.Items.Select(a => new ActivityDTO
            {
                Id = a.Id,
                Name = a.Name,
                Quantity = a.Quantity,
                NumberOfVolunteer = a.NumberOfVolunteer,
                StartDate = a.StartDate,
                Description = a.Description,
                Status = a.Status,
                Event = new EventDTO
                {
                    Id = a.Event.Id,
                    Name = a.Event.Name,
                    StartDate = a.Event.StartDate,
                    EndDate = a.Event.EndDate,
                    Description = a.Event.Description,
                    MaxAttendees = a.Event.MaxAttendees,
                    Status = a.Event.Status.ToString() // Assuming Status is an Enum and needs to be mapped to string
                }
            }).ToList();

            return Result.Success(new Success<PagedResult<GetEventActivityByIdDTO.ActivityDTO>>(MessagesList.GetEventActivitySucess.GetMessage().Code, MessagesList.GetEventActivitySucess.GetMessage().Message, new PagedResult<GetEventActivityByIdDTO.ActivityDTO>(resultItems, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)));
        
        }
    }
}
