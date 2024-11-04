using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Presentation.Abstractions;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Services.Donate;


using static PawFund.Contract.Services.Accounts.Filter;
using static PawFund.Contract.Services.Donates.Filter;
using PawFund.Contract.Services.Donate;


namespace PawFund.Presentation.Controller.V1
{
    public class AdminController : ApiController
    {
        public AdminController(ISender sender) : base(sender)
        {
        }

        [HttpPost("ban_user", Name = "BanUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BanUserById([FromForm] Contract.Services.Admins.Command.BanUserCommand ChangeStatus)
        {
            var result = await Sender.Send(ChangeStatus);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpPost("unban_user", Name = "UnbanUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnbanUserById([FromForm] Contract.Services.Admins.Command.UnBanUserCommand ChangeStatus)
        {
            var result = await Sender.Send(ChangeStatus);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_list_user", Name = "GetListUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers([FromQuery] AccountsFilter filterParams,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string[] selectedColumns = null)
        {
            var result = await Sender.Send(new Contract.Services.Accounts.Query.GetUsersQueryHandler(pageIndex, pageSize, filterParams, selectedColumns));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_list_user_donate", Name = "GetListUserDonateAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersDonate([FromQuery] DonateFilter filterParams,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string[] selectedColumns = null)
        {
            var result = await Sender.Send(new Contract.Services.Donate.Query.GetDonatesQuery(pageIndex, pageSize, filterParams, selectedColumns));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
