using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Contract.Services.HistoryCats;

public static class Response
{
    public record HistoryCatResponse(Guid Id, DateTime DateAdopt, Guid CatId, Guid AccountId);
}
