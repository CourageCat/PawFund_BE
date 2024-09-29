using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Authentications;
using PawFund.Presentation.Abstractions;

namespace PawFund.Presentation.Controller.V1;

public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender sender) : base(sender)
    {}

    [HttpPost("register", Name = "Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] Command.RegisterCommand CreateUser)
    {
        var result = await Sender.Send(CreateUser);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("verify_email", Name = "VerifyEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> VerifyEmail([FromQuery] string email)
    {
        var result = await Sender.Send(new Command.VerifyEmail(email));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("get", Name = "Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        return Ok("Hello world");
    }

    [HttpPost("login", Name = "LoginQuery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] Query.LoginQuery Login)
    {
        var result = await Sender.Send(Login);
        if (result.IsFailure)
            return HandlerFailure(result);

        var value = result.Value;

        Response.Cookies.Append("refreshToken", value.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Path = "/",
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddMinutes(131400),
        });

        return Ok(new
        {
           UserId = value.Id,
           value.FirstName,
           value.LastName,
           TokenType = "Bearer",
           value.AccessToken,
        });
    }
}
