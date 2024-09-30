using System.Security.Claims;

namespace PawFund.Contract.Abstractions;

public interface ITokenGeneratorService
{
    string GenerateToken
        (string secretKey, string issuer, string audience, double expirationMinutes, IEnumerable<Claim>? claims = null);
    string GenerateAccessToken(Guid userId, int roleName);
    string GenerateRefreshToken(Guid userId, int roleName);
    string ValidateAndGetUserIdFromRefreshToken(string refreshToken);
}
