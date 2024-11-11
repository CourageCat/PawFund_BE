﻿using PawFund.Contract.DTOs.Account;
using PawFund.Contract.Enumarations.Authentication;
using System.Security.Principal;

namespace PawFund.Contract.Services.Accounts;
public static class Response
{
    public record UserResponse(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber, GenderType Gender, LoginType? LoginType = LoginType.Local);
    public record UsersResponse(Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    bool IsDeleted,
    LoginType LoginType,
    GenderType Gender,
    string? CropAvatarUrl,
    string? CropAvatarId,
    string? FullAvatarUrl,
    string? FullAvatarId,
    DateTime? CreatedAt);    
}
