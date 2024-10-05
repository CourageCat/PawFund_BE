

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


        //[HttpGet("get_all_users", Name = "GetAllUser")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetListUser([FromBody] Query.GetAllUser ListUser)
        //{
        //    var result = await Sender.Send(ListUser);
        //    if (result.IsFailure)
        //        return HandlerFailure(result);

        //    return Ok(result);
        //}

        [HttpPost("change_status_user", Name = "ChangeUserStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BanUserById([FromBody] Contract.Services.Admins.Command.ChangeUserStatusCommand ChangeStatus)
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
            var result = await Sender.Send(new Query.GetUserById(id));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        //[HttpGet("get_list_user", Name = "GetListUser")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetListUser()
        //{
        //    var result = await Sender.Send(new Query.GetUserById());
        //    if (result.IsFailure)
        //        return HandlerFailure(result);

        //    return Ok(result);
        //}
    }
}
