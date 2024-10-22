using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Adopt.Response;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.AdoptApplication;
public sealed class GetMeetingTimeByStaffQueryHandler : IQueryHandler<Query.GetMeetingTimeByStaffQuery, Success<Response.GetMeetingTimeByStaffResponse>>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IRepositoryBase<Account, Guid> _accountRepository;

    public GetMeetingTimeByStaffQueryHandler(IResponseCacheService responseCacheService, IRepositoryBase<Account, Guid> accountRepository)
    {
        _responseCacheService = responseCacheService;
        _accountRepository = accountRepository;
    }

    public async Task<Result<Success<Response.GetMeetingTimeByStaffResponse>>> Handle(Query.GetMeetingTimeByStaffQuery request, CancellationToken cancellationToken)
    {
        //Find staff account
        var staffFound = await _accountRepository.FindByIdAsync(request.AccountId);
        if (staffFound == null)
        {
            throw new AuthenticationException.UserNotFoundByIdException(request.AccountId);
        }
        //Get branch name based on Staff Account
        var branchName = staffFound.Branches.FirstOrDefault().Name;
        var listMeetingTime = await _responseCacheService.GetListAsync<GetMeetingTimeByStaffResponseDTO.MeetingTimeDTO>(branchName);
        //Check if list empty then return empty message
        if (listMeetingTime == null)
        {
            //Create a new empty list meeting time
            listMeetingTime = new List<GetMeetingTimeByStaffResponseDTO.MeetingTimeDTO>();
            var resultNotFound = new Response.GetMeetingTimeByStaffResponse(listMeetingTime);
            //Return Empty Result
            return Result.Success(new Success<Response.GetMeetingTimeByStaffResponse>(MessagesList.AdoptNotFoundAnyMeetingTimeException.GetMessage().Code, MessagesList.AdoptNotFoundAnyMeetingTimeException.GetMessage().Message, resultNotFound));
        }

        //Return List Result
        var result = new Response.GetMeetingTimeByStaffResponse(listMeetingTime.Where(x => x.MeetingTime > DateTime.Now).ToList()); 
        return Result.Success(new Success<Response.GetMeetingTimeByStaffResponse>(MessagesList.AdoptGetAllMeetingTimeSuccess.GetMessage().Code, MessagesList.AdoptGetAllMeetingTimeSuccess.GetMessage().Message, result));
    }
}
