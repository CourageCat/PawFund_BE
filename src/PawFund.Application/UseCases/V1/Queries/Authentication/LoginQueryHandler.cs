using PawFund.Contract.Abstractions;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Authentications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
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
        if (account == null) throw new EmailNotFoundException();
        
        var isVerifyPassword = _passwordHashService.VerifyPassword(request.Password, account.Password);
        if (isVerifyPassword == false) throw new PasswordNotMatchException();

        var accessToken = _tokenGeneratorService.GenerateAccessToken(account.Id, account.RoleId);
        var refrehsToken = _tokenGeneratorService.GenerateRefreshToken(account.Id, account.RoleId);

        return Result.Success
            (new Response.LoginResponse
            (account.Id,
            account.FirstName,
            account.LastName,
            accessToken,
            refrehsToken));
    }
}
