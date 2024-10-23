using Microsoft.AspNetCore.Http;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Account;
using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Contract.Services.Accounts
{
    public static class Command
    {
        public record UpdateInfoCommand(Guid UserId, string FirstName, string LastName, string PhoneNumber, GenderType Gender) : ICommand<Success<Response.UserResponse>>;
        public record UpdateAvatarCommand(Guid UserId, IFormFile CropAvatarFile, IFormFile FullAvatarFile) : ICommand<Success<AccountAvatarDto>>;
    }
}
