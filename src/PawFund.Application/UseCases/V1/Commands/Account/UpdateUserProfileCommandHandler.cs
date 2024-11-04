using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Persistence;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Commands.Account;

public sealed class UpdateInfoProfileCommandHandler : ICommandHandler<Command.UpdateInfoCommand, Success<Response.UserResponse>>
{
    private readonly IEFUnitOfWork _efUnitOfWork;

    public UpdateInfoProfileCommandHandler(IEFUnitOfWork efUnitOfWork)
    {
        _efUnitOfWork = efUnitOfWork;
    }

    public async Task<Result<Success<Response.UserResponse>>> Handle(Command.UpdateInfoCommand request, CancellationToken cancellationToken)
    {
        var result = await _efUnitOfWork.AccountRepository.FindByIdAsync(request.UserId);
        if (result == null)
            throw new AccountNotFoundException();
        
        result.UpdateInfoProfileUser(request.FirstName, request.LastName, request.PhoneNumber, request.Gender);
        _efUnitOfWork.AccountRepository.Update(result);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);

        var response = new Response.UserResponse(result.Id, result.FirstName, result.LastName, result.Email, result.PhoneNumber, result.Gender);
        
        return Result.Success(new Success<Response.UserResponse>
            (MessagesList.AccountUpdateInformationSuccess.GetMessage().Code,
            MessagesList.AccountUpdateInformationSuccess.GetMessage().Message,
            response));
    }
}
