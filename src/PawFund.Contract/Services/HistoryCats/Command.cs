using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Contract.Services.HistoryCats;
public static class Command
{
    public record CreateHistoryCatCommand(DateTime DateAdopt, Guid CatId, Guid AccountId) : ICommand;
    public record UpdateHistoryCatCommand(Guid Id, DateTime DateAdopt, Guid CatId, Guid AccountId) : ICommand;
    public record DeleteHistoryCatCommand(Guid Id) : ICommand;
}

