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

    [Authorize(Policy = "MemberPolicy")]
    [HttpPut("update-info-profile", Name = "UpdateInfoProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateInfoProfile([FromBody] AccountRequest.UpdateInfoProfileRequestDto request)
    {
        var userId = User.FindFirstValue("UserId");
        var result = await Sender.Send(new Command.UpdateInfoCommand(Guid.Parse(userId), request.FirstName, request.LastName, request.PhoneNumber, request.Gender));
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

    [Authorize(Policy = "MemberPolicy")]
    [HttpGet("get-account-profile", Name = "GetAccountProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountProfile()
    {
        var userId = User.FindFirstValue("UserId");
        var result = await Sender.Send(new Query.GetUserProfileQuery(Guid.Parse(userId)));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize(Policy = "MemberPolicy")]
    [HttpPut("update-email-profile", Name = "UpdateEmailProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmailProfile([FromBody] AccountRequest.UpdateEmailRequestDto request)
    {
        var userId = User.FindFirstValue("UserId");
        var result = await Sender.Send(new Command.UpdateEmailCommand(Guid.Parse(userId), request.Email));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPut("verify-update-email", Name = "VerifyUpdateEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmailProfile([FromQuery] string userId)
    {
        var result = await Sender.Send(new Command.VerifyUpdateEmailCommand(Guid.Parse(userId)));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}
