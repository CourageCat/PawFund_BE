using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Event;
using PawFund.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Presentation.Controller.V1
{
    public class EventController : ApiController
    {
        public EventController(ISender sender) : base(sender)
        {
        }

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

        [HttpGet("get_event_by_id", Name = "GetEventById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventById([FromQuery] Guid Id)
        {
            var result = await Sender.Send(new Query.GetEventByIdQuery(Id));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpPut(Name = "UpdateEventById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEventById([FromBody] Command.UpdateEventCommand updateEvent)
        {
            var result = await Sender.Send(updateEvent);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpDelete(Name = "DeleteEventById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventById([FromBody] Command.DeleteEventCommand Id)
        {
            var result = await Sender.Send(Id);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet(Name = "GetAllEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEvent()
        {
            var result = await Sender.Send(new Query.GetAllEvent());
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
