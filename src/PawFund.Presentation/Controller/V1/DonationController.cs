using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Presentation.Controller.V1
{
    public class DonationController : ApiController
    {
        public DonationController(ISender sender) : base(sender)
        {
        }

        //[HttpPost("create_donation", Name = "CreateDonation")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> CreateDonation([FromForm] decimal amount, string description, Guid paymentMethodId)
        //{
        //    var userId = User.FindFirstValue("UserId");
        //    var result = await Sender.Send(new(Contract.Services.Donors.Command.CreateDonationCommand(amount, description, paymentMethodId, Guid.Parse(userId));
        //    if (result.IsFailure)
        //        return HandlerFailure(result);

        //    return Ok(result);
        //}
    }
}
