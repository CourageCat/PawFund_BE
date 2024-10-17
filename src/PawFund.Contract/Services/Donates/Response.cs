namespace PawFund.Contract.Services.Donate;

public static class Response
{
    public record SuccessDonateBankingResponse(string SuccessUrl);
    public record FailDonateBankingResponse(string FailUrl);
}
