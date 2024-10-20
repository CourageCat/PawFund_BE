using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Presentation.Abstractions;

namespace PawFund.Presentation.Controller.V1;

public class AccountController : ApiController
{
    public AccountController(ISender sender) : base(sender)
    {
    }

    [HttpPut("update_profile", Name = "UpdateProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfile([FromForm] Contract.Services.Accounts.Command.UpdateUserCommand ChangeStatus)
    {
        var result = await Sender.Send(ChangeStatus);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}
