using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Presentation.Abstractions;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PawFund.Presentation.Controller.V1;

public class AccountController : ApiController
{
    public AccountController(ISender sender) : base(sender)
    {
    }

    [HttpPut("update_profile", Name = "UpdateProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfile([FromForm] Command.UpdateUserCommand ChangeStatus)
    {
        var result = await Sender.Send(ChangeStatus);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize(Policy = "MemberPolicy")]
    [HttpPut("update-avatar-profile", Name = "UpdateAvatarProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAvatarProfile([FromForm] AccountRequest.UpdateAvatarRequestDto request)
    {
        var userId = User.FindFirstValue("UserId");
        var result = await Sender.Send(new Command.UpdateAvatarCommand(Guid.Parse(userId), request.CropAvatar, request.FullAvatar));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}
