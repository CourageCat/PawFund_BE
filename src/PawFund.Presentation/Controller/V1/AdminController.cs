

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Presentation.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using PawFund.Contract.Services.Admins;
using PawFund.Contract.Services.Accounts;

namespace PawFund.Presentation.Controller.V1
{
    public class AdminController : ApiController
    {
        public AdminController(ISender sender) : base(sender)
        {
        }

        [HttpPost("change_status_user", Name = "ChangeUserStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BanUserById([FromForm] Contract.Services.Admins.Command.ChangeUserStatusCommand ChangeStatus)
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
        public async Task<IActionResult> GetListUser()
        {
            var result = await Sender.Send(new Query.GetListUserQuery());
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
