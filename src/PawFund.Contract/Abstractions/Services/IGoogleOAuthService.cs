using PawFund.Contract.DTOs.AuthenticationDTOs;

namespace PawFund.Contract.Abstractions.Services;

public interface IGoogleOAuthService
{
    Task<GoogleUserInfoDTO> ValidateTokenAsync(string AccessTokenGoogle);
}
