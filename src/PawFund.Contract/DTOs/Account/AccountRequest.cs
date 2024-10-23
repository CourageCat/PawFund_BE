using Microsoft.AspNetCore.Http;
using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Contract.DTOs.Account;

public static class AccountRequest
{
    public record UpdateAvatarRequestDto(IFormFile CropAvatar, IFormFile FullAvatar);
    public record UpdateInfoProfileRequestDto(string FirstName, string LastName, string PhoneNumber, GenderType Gender);
    public record UpdateEmailRequestDto(string Email);
}
