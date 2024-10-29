using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PawFund.Contract.DTOs.Adopt.Request;
using PawFund.Contract.DTOs.DonateDTOs;
using PawFund.Contract.DTOs.PaymentDTOs;
using PawFund.Contract.Services.Donate;
using PawFund.Contract.Services.Donates;
using PawFund.Presentation.Abstractions;
using System.Security.Claims;
using static PawFund.Contract.Services.Donates.Filter;
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

    [Authorize(Policy = "MemberPolicy")]
    [HttpGet("get-user-donates", Name = "GetUserDonates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserDonates(
    [FromQuery] Filter.DonateFilter filterParams,
    [FromQuery] int pageIndex = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string[] selectedColumns = null)
    {
        var userId = User.FindFirstValue("UserId");
        Filter.DonateFilter filter = new DonateFilter(PaymentMethodType: filterParams.PaymentMethodType, MinAmount: filterParams.MinAmount, MaxAmount: filterParams.MaxAmount, UserId: Guid.Parse(userId), IsDateDesc: filterParams.IsDateDesc);
        var result = await Sender.Send(new Query.GetDonatesQuery(pageIndex, pageSize, filter, selectedColumns));
        if (result.IsFailure)
            return HandlerFailure(result);
        
        return Ok(result);
    }

    [HttpPost("create_donate_cash", Name = "CreateDonateCash")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateDonateCash([FromForm] Command.CreateDonateCash CashDonate)
    {
        var result = await Sender.Send(CashDonate);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_donate", Name = "GetDonate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDonate([FromQuery] long orderId)
    {
        var result = await Sender.Send(new Query.GetDonateByOrderIdQuery(orderId));
        if (result.IsFailure)
            return HandlerFailure(result);
        
        return Ok(result);
    }
}
