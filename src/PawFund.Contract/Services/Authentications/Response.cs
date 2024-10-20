namespace PawFund.Contract.Services.Authentications;

public static class Response
{
    public record LoginResponse
        (Guid UserId,
        string FirstName,
        string LastName,
        string AvatarLink,
        int RoleId,
        string AccessToken,
        string RefreshToken);

    public record RefreshTokenResponse
        (string AccessToken,
        string RefreshToken);
}