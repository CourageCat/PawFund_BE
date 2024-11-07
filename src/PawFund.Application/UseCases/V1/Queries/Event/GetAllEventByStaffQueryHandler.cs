using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.BranchDTOs;
using PawFund.Contract.DTOs.EventDTOs.Respone;
using PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Domain.Exceptions.BranchException;
using static PawFund.Domain.Exceptions.EventException;

namespace PawFund.Application.UseCases.V1.Queries.Event
{
    public sealed class GetAllEventByStaffQueryHandler : IQueryHandler<Query.GetAllEventByStaff, Success<PagedResult<EventForAdminStaffDTO>>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllEventByStaffQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
        {
            _dpUnitOfWork = dpUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Success<PagedResult<EventForAdminStaffDTO>>>> Handle(Query.GetAllEventByStaff request, CancellationToken cancellationToken)
        {
            //get all branchId from accountId and check
            List<Guid> listBranchId = await _dpUnitOfWork.BranchRepositories.GetAllBranchByAccountId(request.staffId);

            if (listBranchId.Count == 0)
            {
                throw new BranchNotFoundOfStaffException(request.staffId);
            }

            var result = await _dpUnitOfWork.EventRepository.GetAllEventByStaff(listBranchId,request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);

            if (result != null && result.Items != null && result.Items.Count > 0)
            {
                //var eventDtos = result.Items
                //    .Where(item => item != null)
                //    .Select(item => new EventDTO
                //    {
                //        Id = item.Id,
                //        Name = item.Name,
                //        StartDate = item.StartDate,
                //        EndDate = item.EndDate,
                //        Description = item.Description,
                //        Status = item.Status.ToString(),
                //        MaxAttendees = item.MaxAttendees,
                //        ImagesUrl = item.ImagesUrl,
                //        Branch = item.Branch != null ? new BranchEventDTO
                //        {
                //            Id = item.Branch.Id,
                //            Name = item.Branch.Name,
                //            PhoneNumberOfBranch = item.Branch.PhoneNumberOfBranch,
                //            Description = item.Branch.Description,
                //            EmailOfBranch = item.Branch.EmailOfBranch,
                //            NumberHome = item.Branch.NumberHome,
                //            StreetName = item.Branch.StreetName,
                //            Ward = item.Branch.Ward,
                //            District = item.Branch.District,
                //            Province = item.Branch.Province,
                //        } : null
                //     }).ToList();

                var eventDtos = _mapper.Map<List<EventForAdminStaffDTO>>(result.Items);
                return Result.Success(new Success<PagedResult<EventForAdminStaffDTO>>(
                    MessagesList.GetAllEventByStaffSuccess.GetMessage().Code,
                    MessagesList.GetAllEventByStaffSuccess.GetMessage().Message,
                    new PagedResult<EventForAdminStaffDTO>(eventDtos, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)
                ));
            }
            else
            {
                throw new EventNotFoundByStaffException(request.staffId);
            }
        }
    }
}
