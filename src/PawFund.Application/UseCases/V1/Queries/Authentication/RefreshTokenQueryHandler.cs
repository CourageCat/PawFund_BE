using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using static PawFund.Domain.Exceptions.AuthenticationException;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Queries.Authentication;

public class RefreshTokenQueryHandler : IQueryHandler<Query.RefreshTokenQuery, Response.RefreshTokenResponse>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IDPUnitOfWork _dPUnitOfWork;

    public RefreshTokenQueryHandler
        (IResponseCacheService responseCacheService,
        ITokenGeneratorService tokenGeneratorService,
        IDPUnitOfWork dPUnitOfWork)
    {
        _responseCacheService = responseCacheService;
        _tokenGeneratorService = tokenGeneratorService;
        _dPUnitOfWork = dPUnitOfWork;
    }

    public async Task<Result<Response.RefreshTokenResponse>> Handle
        (Query.RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        // Check refresh token and return userId get in refresh token decoded
        var userId = _tokenGeneratorService.ValidateAndGetUserIdFromRefreshToken(request.Token);
        // If return == null => Exception
        if (userId == null) throw new RefreshTokenNullException();

        var account = await _dPUnitOfWork.AccountRepositories.GetByIdAsync(Guid.Parse(userId));
        // Ban account
        if (account.IsDeleted == true) throw new AccountBanned();

        // Generate accesssToken and refreshToken
        var accessToken = _tokenGeneratorService.GenerateAccessToken(account.Id, (int)account.RoleId);
        var refrehsToken = _tokenGeneratorService.GenerateRefreshToken(account.Id, (int)account.RoleId);

        return Result.Success(
            new Response.RefreshTokenResponse
            (accessToken, refrehsToken));
    }
}
