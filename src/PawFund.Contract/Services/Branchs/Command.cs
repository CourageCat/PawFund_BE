using Microsoft.AspNetCore.Http;
using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Branchs;
public static class Command
{
    public record CreateBranchCommand(string Name, string PhoneNumberOfBranch, string EmailOfBranch, string Description, string NumberHome, string StreetName, string Ward, string District, string Province, string PostalCode, IFormFile? Image) : ICommand;
    public record UpdateBranchCommand(Guid Id, string Name, string PhoneNumberOfBranch, string EmailOfBranch, string Description, string NumberHome, string StreetName, string Ward, string District, string Province, string PostalCode, IFormFile? Image) : ICommand;
    public record DeleteBranchCommand(Guid Id) : ICommand;
}

