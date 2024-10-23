using Microsoft.AspNetCore.Http;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Account;

namespace PawFund.Contract.Services.Accounts
{
    public static class Command
    {
        public record UpdateUserCommand(Guid ID, string FirstName, string LastName, IFormFile? AvatarFile) : ICommand;
        public record UpdateAvatarCommand(Guid UserId, IFormFile CropAvatarFile, IFormFile FullAvatarFile) : ICommand<Success<AccountAvatarDto>>;
    }
}
