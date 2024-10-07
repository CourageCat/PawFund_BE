using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.DTOs.AuthenticationDTOs;
using PawFund.Contract.Services.Authentications;
using PawFund.Presentation.Abstractions;
using static PawFund.Domain.Exceptions.AuthenticationException;

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

    [HttpGet("verify-email", Name = "VerifyEmailCommand")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> VerifyEmail([FromQuery] string email)
    {
        var result = await Sender.Send(new Command.VerifyEmailCommand(email));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
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

        var authProfileDTO = new AuthProfileDTO()
        {
            UserId = value.UserId,
            FirstName = value.FirstName,
            LastName = value.LastName,
        };

        var tokenDto = new TokenDTO()
        {
            AccessToken = value.AccessToken,
            TokenType = "Bearer"
        };

        return Ok(new
        {
            AuthProfile = authProfileDTO,
            Token = tokenDto,
        });
    }

    [HttpGet("refresh-token", Name = "RefreshToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (refreshToken == null) throw new RefreshTokenNull();

        var result = await Sender.Send(new Query.RefreshTokenQuery(refreshToken));
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
            TokenType = "Bearer",
            value.AccessToken,
        });
    }

    [HttpPost("forgot-password-email", Name = "ForgotPasswordEmailCommand")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPasswordEmail([FromBody] Command.ForgotPasswordEmailCommand ForgotPasswordEmail)
    {
        var result = await Sender.Send(ForgotPasswordEmail);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPost("forgot-password-otp", Name = "ForgotPasswordOtpCommand")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPasswordOtp([FromBody] Command.ForgotPasswordOtpCommand ForgotPasswordOtp)
    {
        var result = await Sender.Send(ForgotPasswordOtp);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPost("forgot-password-change", Name = "ForgotPasswordChangeCommand")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPasswordChange([FromBody] Command.ForgotPasswordChangeCommand ForgotPasswordChange)
    {
        var result = await Sender.Send(ForgotPasswordChange);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("get", Name = "get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        return Ok("Hello world");
    }
}
