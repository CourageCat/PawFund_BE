using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Event;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Domain.Exceptions.BranchException;
using static PawFund.Domain.Exceptions.EventException;

namespace PawFund.Application.UseCases.V1.Queries.Event
{
    public sealed class GetAllEventByAdminQueryHandler : IQueryHandler<Query.GetAllEventByAdmin, Success<PagedResult<EventForAdminStaffDTO>>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllEventByAdminQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
        {
            _dpUnitOfWork = dpUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Success<PagedResult<EventForAdminStaffDTO>>>> Handle(Query.GetAllEventByAdmin request, CancellationToken cancellationToken)
        {
            PagedResult<Domain.Entities.Event>? result;

            //check if query by staffId or not
            if (request.StaffId != null)
            {
                Guid nonNullStaffId = request.StaffId ?? Guid.Empty;
                List<Guid> listBranchId = await _dpUnitOfWork.BranchRepositories.GetAllBranchByAccountId(nonNullStaffId);

                if (listBranchId.Count == 0)
                {
                    throw new BranchNotFoundOfStaffException(nonNullStaffId);
                }

                result = await _dpUnitOfWork.EventRepository.GetAllEventByStaff(listBranchId, request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);

                if (result != null && result.Items != null && result.Items.Count > 0)
                {
                    var eventDtos = _mapper.Map<List<EventForAdminStaffDTO>>(result.Items);
                    return Result.Success(new Success<PagedResult<EventForAdminStaffDTO>>(
                        MessagesList.GetAllEventByStaffSuccess.GetMessage().Code,
                        MessagesList.GetAllEventByStaffSuccess.GetMessage().Message,
                        new PagedResult<EventForAdminStaffDTO>(eventDtos, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)
                    ));
                }
                else
                {
                    throw new EventNotFoundByStaffException(nonNullStaffId);
                }
            }
            else
            {
                result = await _dpUnitOfWork.EventRepository.GetAllEventAsync(request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);
                if (result != null && result.Items != null && result.Items.Count > 0)
                {
                    var eventDtos = _mapper.Map<List<EventForAdminStaffDTO>>(result.Items);
                    return Result.Success(new Success<PagedResult<EventForAdminStaffDTO>>(
                        MessagesList.GetAllEventByStaffSuccess.GetMessage().Code,
                        MessagesList.GetAllEventByStaffSuccess.GetMessage().Message,
                        new PagedResult<EventForAdminStaffDTO>(eventDtos, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)
                    ));
                }
            }

            //return to fe
            if (result != null && result.Items != null && result.Items.Count > 0)
            {
                var eventDtos = _mapper.Map<List<EventForAdminStaffDTO>>(result.Items);
                return Result.Success(new Success<PagedResult<EventForAdminStaffDTO>>(
                    MessagesList.GetAllEventByAdminSuccess.GetMessage().Code,
                    MessagesList.GetAllEventByAdminSuccess.GetMessage().Message,
                    new PagedResult<EventForAdminStaffDTO>(eventDtos, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)
                ));
            }
            else
            {
                throw new EventNotFoundByAdminException();
            }

        }
    }
}
