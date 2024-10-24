using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication;

public class UpdateMeetingTimeCommandHandler : ICommandHandler<Command.UpdateMeetingTimeCommand>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IRepositoryBase<Domain.Entities.Account, Guid> _accountRepository;
    
    public UpdateMeetingTimeCommandHandler(IResponseCacheService responseCacheService, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository)
    {
        _responseCacheService = responseCacheService;
        _accountRepository = accountRepository;
    }

    public async Task<Result> Handle(Command.UpdateMeetingTimeCommand request, CancellationToken cancellationToken)
    {
        //Find staff account
        var staffFound = await _accountRepository.FindByIdAsync(request.AccountId);
        if (staffFound == null)
        {
            throw new AuthenticationException.UserNotFoundByIdException(request.AccountId);
        }
        //Get branch name based on Staff Account
        var branchName = staffFound.Branches.FirstOrDefault().Name;
        //Check if request does not have any meeting time then remove key
        if (request.ListTime.Any())
        {
            //Find farthest time and set expired time for key in redis
            //ExpiredTime = farthestTime - DateTime.Now + 0.5Day
            var fartheseTime = request.ListTime.Max(x => x.MeetingTime);
            var expiredTime = (fartheseTime - DateTime.Now).Add(TimeSpan.FromDays(0.5));

            //Add time to Redis (Key: Branch Name, Value: List of time)
            await _responseCacheService.SetListAsync(branchName, request.ListTime, expiredTime);
        }
        else
        {
            //Remove key
            await _responseCacheService.DeleteCacheResponseAsync(branchName);
        }

        //Return result
        return Result.Success(new Success(MessagesList.AdoptUpdateMeetingTimeSuccess.GetMessage().Code, MessagesList.AdoptUpdateMeetingTimeSuccess.GetMessage().Message));
    }
}
