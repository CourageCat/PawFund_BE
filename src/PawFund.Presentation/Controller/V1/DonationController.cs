using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Donate;
using PawFund.Presentation.Abstractions;
namespace PawFund.Presentation.Controller.V1;

public class DonationController : ApiController
{
    public DonationController(ISender sender) : base(sender)
    {
    }

    [HttpPost("donate-banking", Name = "DonateBanking")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] Command.CreateDonationBankingCommand CreateDonateBanking)
    {
        var result = await Sender.Send(CreateDonateBanking);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

}
