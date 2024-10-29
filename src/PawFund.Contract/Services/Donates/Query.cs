using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.DonateDTOs;
using PawFund.Contract.Services.Donates;

namespace PawFund.Contract.Services.Donate;

public static class Query
{
    public record SuccessDonateBankingQuery(long OrderId) : IQuery<Response.SuccessDonateBankingResponse>;
    public record FailDonateBankingQuery(long OrderId) : IQuery<Response.FailDonateBankingResponse>;

    public record GetDonatesQuery(int PageIndex,
        int PageSize,
        Filter.DonateFilter FilterParams,
        string[] SelectedColumns) : IQuery<Success<PagedResult<DonateDto>>>;

    public record GetDonateByOrderIdQuery(long OrderId) : IQuery<Success<DonateDto>>;
}
