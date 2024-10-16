using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Presentation.Abstractions;
using System.Security.Claims;

namespace PawFund.Presentation.Controller.V1
{
    public class VolunteerApplictionDetailController : ApiController
    {
        public VolunteerApplictionDetailController(ISender sender) : base(sender)
        {
        }
        
        [HttpPost("create_volunteer_application", Name = "CreateVolunteerApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateVolunteerApplication([FromBody] Command.FormRegisterVolunteer form)
        {
            var userId = User.FindFirstValue("UserId");
            var result = await Sender.Send(new Command.CreateVolunteerApplicationDetail(form, Guid.Parse(userId)));
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

        [HttpPut("approve_volunteer_application", Name = "ApproveVolunteerApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveVolunteerApplication([FromBody] Command.ApproveVolunteerApplication id)
        {
            var result = await Sender.Send(id);
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

        [HttpPut("reject_volunteer_application", Name = "RejectVolunteerApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RejectVolunteerApplication([FromBody] Command.RejectVolunteerApplication reject)
        {
            var result = await Sender.Send(reject);
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }
    }
}
