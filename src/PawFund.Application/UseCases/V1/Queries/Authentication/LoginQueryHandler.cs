using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Contract.Enumarations.Authentication;
using static PawFund.Domain.Exceptions.AuthenticationException;

namespace PawFund.Application.UseCases.V1.Queries.Authentication;

public sealed class LoginQueryHandler : IQueryHandler<Query.LoginQuery, Response.LoginResponse>
{
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IPasswordHashService _passwordHashService;

    public LoginQueryHandler
        (ITokenGeneratorService tokenGeneratorService,
        IDPUnitOfWork dpUnitOfWork,
        IPasswordHashService passwordHashService)
    {
        _tokenGeneratorService = tokenGeneratorService;
        _dpUnitOfWork = dpUnitOfWork;
        _passwordHashService = passwordHashService;
    }

    public async Task<Result<Response.LoginResponse>> 
        Handle(Query.LoginQuery request, CancellationToken cancellationToken)
    {
        var account = await _dpUnitOfWork.AccountRepositories.GetByEmailAsync(request.Email);
        // If account == null => Exception
        if (account == null) throw new EmailNotFoundException();
        // If account have login type != Local => Exception
        if (account.LoginType != LoginType.Local)
            throw new AccountRegisteredAnotherMethodException();

        if (account.IsDeleted == true) throw new AccountBanned();

        // Check password have equal with password hashed
        var isVerifyPassword = _passwordHashService.VerifyPassword(request.Password, account.Password);
        // If password not equal
        if (isVerifyPassword == false) throw new PasswordNotMatchException();
        
        // Generate accessToken and refreshToken
        var accessToken = _tokenGeneratorService.GenerateAccessToken(account.Id, (int)account.RoleId);
        var refrehsToken = _tokenGeneratorService.GenerateRefreshToken(account.Id, (int)account.RoleId);
        
        return Result.Success
            (new Response.LoginResponse
            (account.Id,
            account.FirstName,
            account.LastName,
            account.CropAvatarUrl,
            account.FullAvatarUrl,
            (int)account.RoleId,
            accessToken,
            refrehsToken));
    }
}
