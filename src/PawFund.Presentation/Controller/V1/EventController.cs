
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Events;
using PawFund.Presentation.Abstractions;

namespace PawFund.Presentation.Controller.V1
{
    [ApiVersion(1)]
    public class EventController : ApiController
    {
        public EventController(ISender sender) : base(sender)
        { }

        [HttpPost("create_event", Name = "CreateEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEvents([FromBody] Command.CreateEventCommand CreateEvent)
        {
            var result = await Sender.Send(CreateEvent);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_event_by_id",Name = "GetEventById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventsById([FromQuery] Query.GetEventById getEventById)
        {
            var result = await Sender.Send(getEventById);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
