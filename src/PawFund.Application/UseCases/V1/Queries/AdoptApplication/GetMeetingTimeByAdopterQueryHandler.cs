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
public sealed class GetMeetingTimeByAdopterQueryHandler : IQueryHandler<Query.GetMeetingTimeByAdopterQuery, Success<Response.GetMeetingTimeByAdopterResponse>>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptRepository;

    public GetMeetingTimeByAdopterQueryHandler(IResponseCacheService responseCacheService, IRepositoryBase<AdoptPetApplication, Guid> adoptrepository)
    {
        _responseCacheService = responseCacheService;
        _adoptRepository = adoptrepository;
    }

    public async Task<Result<Success<Response.GetMeetingTimeByAdopterResponse>>> Handle(Query.GetMeetingTimeByAdopterQuery request, CancellationToken cancellationToken)
    {
        //Find Adopt Application
        var applicationFound = await _adoptRepository.FindByIdAsync(request.AdoptId);
        if (applicationFound == null)
        {
            throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
        }
        //Get branch name based on adopt application
        var branchName = applicationFound.Cat.Branch.Name;
        var listMeetingTime = await _responseCacheService.GetListAsync<GetMeetingTimeByAdopterResponseDTO.MeetingTimeDTO>(branchName);
        //Check if list empty then return empty message
        if (listMeetingTime == null)
        {
            //Create a new empty list meeting time
            var listMeetingTimeNotFound = new List<DateTime>();
            var resultNotFound = new Response.GetMeetingTimeByAdopterResponse(listMeetingTimeNotFound);
            //Return Empty Result
            return Result.Success(new Success<Response.GetMeetingTimeByAdopterResponse>(MessagesList.AdoptNotFoundAnyMeetingTimeException.GetMessage().Code, MessagesList.AdoptNotFoundAnyMeetingTimeException.GetMessage().Message, resultNotFound));
        }
        var result = new Response.GetMeetingTimeByAdopterResponse(listMeetingTime.Where(x => x.NumberOfStaffsFree > 0 && x.MeetingTime > DateTime.Now).Select(x => x.MeetingTime).ToList());
        return Result.Success(new Success<Response.GetMeetingTimeByAdopterResponse>(MessagesList.AdoptGetAllMeetingTimeSuccess.GetMessage().Code, MessagesList.AdoptGetAllMeetingTimeSuccess.GetMessage().Message, result));
    }
}
