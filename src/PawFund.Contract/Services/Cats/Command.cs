using Microsoft.AspNetCore.Http;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Enumarations.Cat;
using PawFund.Contract.Shared;

namespace PawFund.Contract.Services.Cats;
public static class Command
{
    public record CreateCatCommand(CatSex Sex, string Name, string Age, string Breed, decimal Weight, string Color, string Description, bool Sterilization, List<IFormFile> Images, Guid? UserId) : ICommand;
    public record UpdateCatCommand(Guid CatId, CatSex? Sex, string? Name, string? Age, string? Breed, decimal? Weight, string? Color, string? Description, bool? Sterilization, List<Guid>? OldImages, List<IFormFile>? NewImages) : ICommand<Success>;
    public record DeleteCatCommand(Guid Id) : ICommand;
}
