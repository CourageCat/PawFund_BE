using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Presentation.Abstractions;
using PawFund.Contract.Services.Accounts;
using static PawFund.Contract.Services.Accounts.Filter;

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

        [HttpGet("get_user_by_id", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById([FromQuery] Guid id)
        {
            var result = await Sender.Send(new Query.GetUserByIdQuery(id));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }


        [HttpGet("get_list_user", Name = "GetListUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromQuery] AccountFilter filterParams,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string[] selectedColumns = null)
        {
            var result = await Sender.Send(new Query.GetUsersQueryHandler(pageIndex, pageSize, filterParams, selectedColumns));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
