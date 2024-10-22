using Microsoft.AspNetCore.Http;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Contract.Services.Cats;
public static class Command
{
    public record CreateCatCommand(CatSex Sex, string Name, string Age, string Breed, decimal Weight, string Color, string Description, Guid BranchId, bool Sterilization, List<IFormFile> Images) : ICommand;
    public record UpdateCatCommand(Guid Id, CatSex Sex, string Name, string Age, string Breed, decimal Size, string Color, string Description) : ICommand;
    public record DeleteCatCommand(Guid Id) : ICommand;
}
