using Microsoft.Extensions.Options;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Services.Donate;
using PawFund.Contract.Settings;
using PawFund.Contract.Shared;

namespace PawFund.Application.UseCases.V1.Queries.Donate;

public sealed class FailDonateBankingQueryHandler : IQueryHandler<Query.FailDonateBankingQuery, Response.FailDonateBankingResponse>
{
    private readonly IResponseCacheService _responseCacheService;
    private readonly ClientSetting _clientSetting;
    private readonly IPaymentService _paymentService;

    public FailDonateBankingQueryHandler(IResponseCacheService responseCacheService,
        IOptions<ClientSetting> clientConfig,
        IPaymentService paymentService)
    {
        _responseCacheService = responseCacheService;
        _clientSetting = clientConfig.Value;
        _paymentService = paymentService;
    }

    public async Task<Result<Response.FailDonateBankingResponse>> Handle(Query.FailDonateBankingQuery request, CancellationToken cancellationToken)
    {
        await _paymentService.CancelOrder((request.OrderId));
        await _responseCacheService.DeleteCacheResponseAsync($"donate_{request.OrderId}");
        return Result.Success(new Response.FailDonateBankingResponse($"{_clientSetting.Url}{_clientSetting.DonateFail}/{request.OrderId}"));
    }
}
