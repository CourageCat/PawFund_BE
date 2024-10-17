using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.DTOs.PaymentDTOs;
using PawFund.Contract.Services.Donate;
using PawFund.Presentation.Abstractions;
using System.Security.Claims;
namespace PawFund.Presentation.Controller.V1;

public class DonationController : ApiController
{
    public DonationController(ISender sender) : base(sender)
    {
    }

    [Authorize(Policy = "MemberPolicy")]
    [HttpPost("donate-banking", Name = "DonateBanking")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DonateBanking([FromBody] CreatePaymentRequestDTO request)
    {
        var userId = User.FindFirstValue("UserId");

        var result = await Sender.Send(new Command.CreateDonationBankingCommand(Guid.Parse(userId), request.Amount, request.Description));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("success-donate-banking", Name = "SuccessDonateBanking")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SuccessDonateBanking([FromQuery] long orderId)
    {
        var result = await Sender.Send(new Query.SuccessDonateBankingQuery(orderId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Redirect(result.Value.SuccessUrl);
    }

    [HttpGet("fail-donate-banking", Name = "FailDonateBanking")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FailDonateBanking([FromQuery] long orderId)
    {
        var result = await Sender.Send(new Query.FailDonateBankingQuery(orderId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Redirect(result.Value.FailUrl);
    }
}
