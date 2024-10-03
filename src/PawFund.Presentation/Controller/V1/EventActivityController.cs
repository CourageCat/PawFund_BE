using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.EventActivities;
using PawFund.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Presentation.Controller.V1
{
    [ApiVersion(1)]
    public class EventActivityController : ApiController
    {
        public EventActivityController(ISender sender) : base(sender)
        {
        }

        [HttpPost("create_event_activity", Name = "CreateEventActivity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEventActivity([FromBody] Command.CreateEventActivityCommand createEventActivity)
        {
            var result = await Sender.Send(createEventActivity);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpPost(Name = "GetEventActivityById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventActivityById([FromBody] Command.CreateEventActivityCommand createEventActivity)
        {
            var result = await Sender.Send(createEventActivity);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
