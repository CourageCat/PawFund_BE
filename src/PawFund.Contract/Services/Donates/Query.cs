using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Donate;

public static class Query
{
    public record SuccessDonateBankingQuery(long OrderId) : IQuery<Response.SuccessDonateBankingResponse>;
    public record FailDonateBankingQuery(long OrderId) : IQuery<Response.FailDonateBankingResponse>;

}
