using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Adopt;

public static class Command
{
    public record CreateAdoptApplicationCommand
        (string Description,
        Guid AccountId,
        Guid CatId)
        : ICommand;

}
