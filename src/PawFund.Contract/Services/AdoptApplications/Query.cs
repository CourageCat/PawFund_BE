using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Adopt.Response;
using static PawFund.Contract.Services.AdoptApplications.Filter;
using static PawFund.Contract.Services.AdoptApplications.Response;

namespace PawFund.Contract.Services.AdoptApplications;

public static class Query
{
    public record GetApplicationByIdQuery
        (Guid Id) : IQuery<Success<GetApplicationByIdResponse>>;

    public record GetAllApplicationQuery(int PageIndex,
        int PageSize,
        bool IsAscCreatedDate,
        string[] SelectedColumns) : IQuery<Success<PagedResult<ApplicationResponse>>>;

    public record GetAllApplicationByAdopterQuery(Guid AccountId, int PageIndex,
        int PageSize,
        AdoptApplicationFilter FilterParams,
        string[] SelectedColumns) : IQuery<Success<PagedResult<ApplicationResponse>>>;

    public record GetAllApplicationByStaffQuery(Guid AccountId, int PageIndex,
        int PageSize,
        AdoptApplicationFilter filterParams,
        string[] SelectedColumns) : IQuery<Success<PagedResult<ApplicationResponse>>>;

    public record GetMeetingTimeByStaffQuery(Guid AccountId) : IQuery<Success<GetMeetingTimeByStaffResponse>>;
    public record GetMeetingTimeByAdopterQuery(Guid AdoptId) : IQuery<Success<GetMeetingTimeByAdopterResponse>>;

}
