
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.DTOs.Event;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Entities;
using static PawFund.Contract.DTOs.Event.GetEventByIdDTO;

namespace PawFund.Application.UseCases.V1.Queries.Event
{
    public sealed class GetAllEventQueryHandler : IQueryHandler<Query.GetAllEvent, List<Respone.EventResponse>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;

        public GetAllEventQueryHandler(IDPUnitOfWork dpUnitOfWork)
        {
            _dpUnitOfWork = dpUnitOfWork;
        }

        public async Task<Result<List<Respone.EventResponse>>> Handle(Query.GetAllEvent request, CancellationToken cancellationToken)
        {
            //generator respone and find all event
            List<Respone.EventResponse> listEvent = new List<Respone.EventResponse>();
            var queryEvent = await _dpUnitOfWork.EventRepository.GetAll();

            if (queryEvent != null)
            {
                // Ánh xạ dữ liệu từ Event sang GetEventByIdDTO.EventDTO và GetEventByIdDTO.BranchDTO
                foreach (var eventItem in queryEvent)
                {
                    var eventDto = new GetEventByIdDTO.EventDTO
                    {
                        Id = eventItem.Id,
                        Name = eventItem.Name,
                        StartDate = eventItem.StartDate,
                        EndDate = eventItem.EndDate,
                        Description = eventItem.Description,
                        MaxAttendees = eventItem.MaxAttendees,
                        Status = eventItem.Status.ToString(),
                    };

                    var branchDto = new GetEventByIdDTO.BranchDTO
                    {
                        Id = eventItem.Branch.Id,
                        Name = eventItem.Branch.Name,
                        PhoneNumberOfBranch = eventItem.Branch.PhoneNumberOfBranch,
                        EmailOfBranch = eventItem.Branch.EmailOfBranch,
                        Description = eventItem.Branch.Description,
                        NumberHome = eventItem.Branch.NumberHome,
                        StreetName = eventItem.Branch.StreetName,
                        Ward = eventItem.Branch.Ward,
                        District = eventItem.Branch.District,
                        Province = eventItem.Branch.Province,
                        PostalCode = eventItem.Branch.PostalCode
                    };

                    var dto = new Respone.EventResponse(eventDto, branchDto);

                    // Thêm DTO vào danh sách
                    listEvent.Add(dto);
                }
            }

            // Trả về kết quả thành công với danh sách DTO
            return Result.Success(listEvent);
        }

        public Task<Result<List<Contract.Services.EventActivity.Respone.EventActivityResponse>>> Handle(Contract.Services.EventActivity.Query.GetApprovedEventsActivityQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
