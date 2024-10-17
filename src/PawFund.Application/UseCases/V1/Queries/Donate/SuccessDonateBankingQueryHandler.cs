using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Services.Donate;
using PawFund.Contract.Settings;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Application.UseCases.V1.Queries.Donate;

public sealed class SuccessDonateBankingQueryHandler : IQueryHandler<Query.SuccessDonateBankingQuery, Response.SuccessDonateBankingResponse>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly IRepositoryBase<Donation, Guid> _donateionRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly ClientSetting _clientSetting;

    public SuccessDonateBankingQueryHandler(
        IResponseCacheService responseCacheService,
        IRepositoryBase<Donation, Guid> donateionRepository,
        IEFUnitOfWork efUnitOfWork,
        IOptions<ClientSetting> clientConfig)
    {
        _responseCacheService = responseCacheService;
        _donateionRepository = donateionRepository;
        _efUnitOfWork = efUnitOfWork;
        _clientSetting = clientConfig.Value;
    }

    public async Task<Result<Response.SuccessDonateBankingResponse>> Handle(Query.SuccessDonateBankingQuery request, CancellationToken cancellationToken)
    {
        // Get infomation saved in memory
        var donateBankingMemory = await _responseCacheService.GetCacheResponseAsync($"donate_{request.OrderId}");
        // Conver JSON to object
        var donateBanking = JsonConvert.DeserializeObject<Command.CreateDonationBankingCommand>(donateBankingMemory);
        // Create object donation
        var donation = Donation.CreateDonationBanking(donateBanking.Amount, donateBanking.Description, request.OrderId, donateBanking.AccountId);

        _donateionRepository.Add(donation);
        await _efUnitOfWork.SaveChangesAsync();

        await _responseCacheService.DeleteCacheResponseAsync($"donate_{request.OrderId}");
        return Result.Success(new Response.SuccessDonateBankingResponse($"{_clientSetting.Url}{_clientSetting.DonateSuccess}/{request.OrderId}"));
    }
}
