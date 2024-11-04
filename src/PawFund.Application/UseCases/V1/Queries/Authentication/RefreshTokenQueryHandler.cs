using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using static PawFund.Domain.Exceptions.AuthenticationException;

namespace PawFund.Application.UseCases.V1.Queries.Authentication;

public class RefreshTokenQueryHandler : IQueryHandler<Query.RefreshTokenQuery, Response.RefreshTokenResponse>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IAccountRepository _accountRepository;

    public RefreshTokenQueryHandler
        (IResponseCacheService responseCacheService,
        ITokenGeneratorService tokenGeneratorService,
        IAccountRepository accountRepository)
    {
        _responseCacheService = responseCacheService;
        _tokenGeneratorService = tokenGeneratorService;
        _accountRepository = accountRepository;
    }

    public async Task<Result<Response.RefreshTokenResponse>> Handle
        (Query.RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        // Check refresh token and return userId get in refresh token decoded
        var userId = _tokenGeneratorService.ValidateAndGetUserIdFromRefreshToken(request.Token);
        // If return == null => Exception
        if (userId == null) throw new RefreshTokenNullException();

        var account = await _accountRepository.GetByIdAsync(Guid.Parse(userId));    

        // Generate accesssToken and refreshToken
        var accessToken = _tokenGeneratorService.GenerateAccessToken(account.Id, (int)account.RoleId);
        var refrehsToken = _tokenGeneratorService.GenerateRefreshToken(account.Id, (int)account.RoleId);

        return Result.Success(
            new Response.RefreshTokenResponse
            (accessToken, refrehsToken));
    }
}
