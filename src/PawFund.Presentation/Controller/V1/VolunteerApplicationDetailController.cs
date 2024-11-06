using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.DTOs.VolunteerApplicationDTOs.Request;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Presentation.Abstractions;
using System.Security.Claims;
using static PawFund.Contract.Services.Event.Filter;
using static PawFund.Contract.Services.VolunteerApplicationDetail.Filter;

namespace PawFund.Presentation.Controller.V1
{
    public class VolunteerApplicationDetailController : ApiController
    {
        public VolunteerApplicationDetailController(ISender sender) : base(sender)
        {
        }

        [Authorize(Policy = "MemberPolicy")]
        [HttpPost("create_volunteer_application", Name = "CreateVolunteerApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateVolunteerApplication([FromBody] CreateVolunteerApplicationDTO form)
        {
            var userId = User.FindFirstValue("UserId");

            var result = await Sender.Send(new Command.CreateVolunteerApplicationDetailCommand(form.eventId,form.listActivity,form.description,Guid.Parse(userId)));

            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

        [HttpPut("approve_volunteer_application", Name = "ApproveVolunteerApplicationCommand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveVolunteerApplication([FromBody] Command.ApproveVolunteerApplicationCommand id)
        {
            var result = await Sender.Send(id);
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

        [HttpPut("reject_volunteer_application", Name = "RejectVolunteerApplicationCommand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RejectVolunteerApplication([FromBody] Command.RejectVolunteerApplicationCommand reject)
        {
            var result = await Sender.Send(reject);
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

        [HttpGet("get_volunteer_application_by_id", Name = "GetVolunteerApplicationByIdCommand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVolunteerApplicationById([FromQuery] Guid id)
        {
            var result = await Sender.Send(new Query.GetVolunteerApplicationByIdQuery(id));
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

        [HttpGet("get_volunteer_application_by_activity_id", Name = "GetVolunteerApplicationByActivityIdCommand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVolunteerApplicationByActivityId([FromQuery] Guid id,
        [FromQuery] VolunteerApplicationFilter filterParams,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string[] selectedColumns = null)
        {
            var result = await Sender.Send(new Query.GetVolunteerApplicationByActivityQuery(id, pageIndex, pageSize, filterParams, selectedColumns));
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }
    }
}
