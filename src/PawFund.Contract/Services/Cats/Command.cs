using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Cats;
public static class Command
{
    public record CreateCatCommand(string Sex, string Name, int Age, string Breed, decimal Size, string Color, string Description, Guid BranchId) : ICommand;
    public record UpdateCatCommand(Guid Id, string Sex, string Name, int Age, string Breed, decimal Size, string Color, string Description) : ICommand;
    public record DeleteCatCommand(Guid Id) : ICommand;
}

