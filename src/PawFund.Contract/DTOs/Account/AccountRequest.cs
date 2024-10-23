using Microsoft.AspNetCore.Http;

namespace PawFund.Contract.DTOs.Account;

public static class AccountRequest
{
    public record UpdateAvatarRequestDto(IFormFile CropAvatar, IFormFile FullAvatar);
}
