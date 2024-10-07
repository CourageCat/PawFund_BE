
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.DTOs.Event;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.Services.EventActivity;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Queries.EventActivity
{
    public sealed class GetAllEventActivityByEventIdQueryHandler : IQueryHandler<Query.GetAllEventActivity, List<Respone.EventActivityResponse>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;

        public GetAllEventActivityByEventIdQueryHandler(IDPUnitOfWork dpUnitOfWork)
        {
            _dpUnitOfWork = dpUnitOfWork;
        }

        public async Task<Result<List<Respone.EventActivityResponse>>> Handle(Query.GetAllEventActivity request, CancellationToken cancellationToken)
        {
            List<Respone.EventActivityResponse> listActivity = new List<Respone.EventActivityResponse>();
            var queryActivity = await _dpUnitOfWork.EventActivityRepositories.GetAllByEventId(request.Id);
            if (queryActivity != null)
            {
                foreach (var activityItem in queryActivity)
                {
                    var activityDTO = new GetEventActivityByIdDTO.ActivityDTO()
                    {
                        Id = activityItem.Id,
                        Description = activityItem.Description,
                        Name = activityItem.Name,
                        StartDate = activityItem.StartDate,
                        Status = activityItem.Status,
                        Quantity = activityItem.Quantity,
                    };

                    var eventDTO = new GetEventActivityByIdDTO.EventDTO
                    {
                        Id = activityItem.Event.Id,
                        Description = activityItem.Event.Description,
                        StartDate = activityItem.Event.StartDate,
                        EndDate = activityItem.Event.EndDate,
                        MaxAttendees = activityItem.Event.MaxAttendees,
                        Name = activityItem.Event.Name,
                    };

                    var dto = new Respone.EventActivityResponse(activityDTO, eventDTO);

                    // Thêm DTO vào danh sách
                    listActivity.Add(dto);
                }
            }

            // Trả về kết quả thành công với danh sách DTO
            return Result.Success(listActivity);
        }
    }
    }
