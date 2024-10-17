using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Contract.Services.Cats;
public static class Command
{
    public record CreateCatCommand(CatSex Sex, string Name, int Age, string Breed, decimal Size, string Color, string Description, Guid BranchId) : ICommand;
    public record UpdateCatCommand(Guid Id, CatSex Sex, string Name, int Age, string Breed, decimal Size, string Color, string Description) : ICommand;
    public record DeleteCatCommand(Guid Id) : ICommand;
}

