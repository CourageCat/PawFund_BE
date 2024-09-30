using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Authentications;

public static class Query
{
    public record LoginQuery
        (string Email,
        string Password) : IQuery<Response.LoginResponse>;

    public record RefreshTokenQuery
        (string Token) : IQuery<Response.RefreshTokenResponse>;
}
