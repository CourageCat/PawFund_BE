using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Adopt.Response;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.EventActivity;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Contract.Services.EventActivity.Respone;

namespace PawFund.Application.UseCases.V1.Queries.EventActivity
{
    public sealed class GetAllEventActivityByEventIdQueryHandler : IQueryHandler<Query.GetAllEventActivity, Success<PagedResult<EventActivityResponse>>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;

        public GetAllEventActivityByEventIdQueryHandler(IDPUnitOfWork dpUnitOfWork)
        {
            _dpUnitOfWork = dpUnitOfWork;
        }

        public async Task<Result<Success<PagedResult<EventActivityResponse>>>> Handle(Query.GetAllEventActivity request, CancellationToken cancellationToken)
        {
            // Lấy danh sách Event Activities từ repository
            var listEventActivityFoundPaging = await _dpUnitOfWork.EventActivityRepositories.GetAllByEventId(
                request.Id, request.pageIndex, request.pageSize, request.filterParams, request.selectedColumns);

            // Tạo danh sách DTO để chứa kết quả chuyển đổi
            var listEventActivityFoundDTO = new List<EventActivityResponse>();

            // Mapping từ entity sang DTO
            listEventActivityFoundPaging.Items.ForEach(eventActivity =>
            {
                var activityDTO = new GetEventActivityByIdDTO.ActivityDTO()
                {
                    Id = eventActivity.Id,
                    Name = eventActivity.Name,
                    Description = eventActivity.Description,
                    StartDate = eventActivity.StartDate,
                    Status = eventActivity.Status,
                    Quantity = eventActivity.Quantity
                };

                var eventDTO = new GetEventActivityByIdDTO.EventDTO()
                {
                    Id = eventActivity.Event.Id,
                    Name = eventActivity.Event.Name,
                    Description = eventActivity.Event.Description,
                    StartDate = eventActivity.Event.StartDate,
                    EndDate = eventActivity.Event.EndDate,
                    MaxAttendees = eventActivity.Event.MaxAttendees,
                    Status = eventActivity.Event.Status.ToString()
                };

                var eventActivityResponse = new EventActivityResponse(activityDTO, eventDTO);

                // Thêm vào danh sách kết quả
                listEventActivityFoundDTO.Add(eventActivityResponse);
            });

            // Tính tổng số trang
            var totalPages = Math.Ceiling((double)listEventActivityFoundPaging.TotalCount / (double)request.pageSize);

            // Trả về kết quả với danh sách DTO và các thông tin phân trang
            var result = new PagedResult<EventActivityResponse>(
                listEventActivityFoundDTO,
                listEventActivityFoundPaging.PageIndex,
                listEventActivityFoundPaging.PageSize,
                listEventActivityFoundPaging.TotalCount,
                totalPages
            );

            if (result.Items.Count == 0)
            {
                return Result.Success(new Success<PagedResult<EventActivityResponse>>(MessagesList.EventActivityEmptyException.GetMessage().Code, MessagesList.EventActivityEmptyException.GetMessage().Message, result));
            }

            //Return result
            return Result.Success(new Success<PagedResult<EventActivityResponse>>(MessagesList.GetEventActivitySucess.GetMessage().Code, MessagesList.GetEventActivitySucess.GetMessage().Message, result));
        

    }
    }
}
