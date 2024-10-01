

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Presentation.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using PawFund.Contract.Services.Admins;

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

        [HttpPost("ban_user", Name = "BanUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BanUserById([FromBody] Contract.Services.Admins.Command.BanUserCommand BanUser)
        {
            var result = await Sender.Send(BanUser);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

    }
}
