using Microsoft.Extensions.Options;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.PaymentDTOs;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Donate;
using PawFund.Contract.Settings;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using System;

namespace PawFund.Application.UseCases.V1.Commands.Donate;

public sealed class CreateDonationBankingCommandHandler : ICommandHandler<Command.CreateDonationBankingCommand>
{
    private readonly IPaymentService _paymentService;
    private readonly PayOSSetting _payOSSetting;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public CreateDonationBankingCommandHandler(
        IPaymentService paymentService,
       IOptions<PayOSSetting> payOSSetting
,
       IResponseCacheService responseCacheService,
       IDPUnitOfWork dpUnitOfWork)
    {
        _paymentService = paymentService;
        _payOSSetting = payOSSetting.Value;
        _responseCacheService = responseCacheService;
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result> Handle(Command.CreateDonationBankingCommand request, CancellationToken cancellationToken)
    {
        // Get latest donation in db to get orderId
        //var donationDB = await _dpUnitOfWork.DonationRepository.GetLatestDonationAsync();

        long orderId = new Random().Next(1, 100000);

        // Create payment dto
        List<ItemDTO> itemDTOs = new List<ItemDTO> { new ItemDTO("Donate", 1, request.Amount) };
        var createPaymentDto = new CreatePaymentDTO(orderId, "Donate", itemDTOs, _payOSSetting.ErrorUrl, _payOSSetting.SuccessUrl + $"?orderId={orderId}");
        var result = await _paymentService.CreatePaymentLink(createPaymentDto);
        // Save memory to when success or fail will know value
        await _responseCacheService.SetCacheResponseAsync($"donate_{orderId}", request,TimeSpan.FromMinutes(60));
        
        return Result.Success(new Success<CreatePaymentResponseDTO>(MessagesList.PaymentSucccess.GetMessage().Code, MessagesList.PaymentSucccess.GetMessage().Message, result));
    }
}
