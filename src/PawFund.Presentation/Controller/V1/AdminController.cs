

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Presentation.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PawFund.Presentation.Controller.V1
{
    public class AdminController : ApiController
    {
        public AdminController(ISender sender) : base(sender)
        {
        }


        [HttpPost("get_all_users",Name = "GetAllUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> User([FromBody] Command.CreateProductCommand CreateProduct)
        {
            var result = await Sender.Send(CreateProduct);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
