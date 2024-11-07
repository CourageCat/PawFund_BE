using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Event;
using static PawFund.Contract.Services.Accounts.Response;
using static PawFund.Contract.Services.Event.Respone;

namespace PawFund.Contract.Services.Event
{
    public static class Query
    {
        public record GetEventByIdQuery
        (Guid Id) : IQuery<Success<Respone.EventResponse>>;

        public record GetAllEvent(int PageIndex,
         int PageSize,
         Filter.EventFilter FilterParams,
         string[] SelectedColumns)
         : IQuery<Success<PagedResult<EventForUserDTO.EventDTO>>>;

        public record GetAllEventByAdmin(Guid? StaffId,
        int PageIndex,
         int PageSize,
         Filter.EventFilter FilterParams,
         string[] SelectedColumns) : IQuery<Success<PagedResult<EventForAdminStaffDTO>>>;

        public record GetAllEventByStaff(Guid staffId,int PageIndex,
         int PageSize,
         Filter.EventFilter FilterParams,
         string[] SelectedColumns)
         : IQuery<Success<PagedResult<EventForAdminStaffDTO>>>;
    }
}
