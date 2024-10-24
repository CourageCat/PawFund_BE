using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Event;
using PawFund.Presentation.Abstractions;

namespace PawFund.Presentation.Controller.V1;
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

    [HttpPut("update_event_by_id", Name = "UpdateEventById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEventById([FromBody] Command.UpdateEventCommand updateEvent)
    {
        var result = await Sender.Send(updateEvent);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpDelete("delete_event_by_id", Name = "DeleteEventById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEventById([FromBody] Command.DeleteEventCommand Id)
    {
        var result = await Sender.Send(Id);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_event", Name = "GetAllEvent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllEvent()
    {
        var result = await Sender.Send(new Query.GetAllEvent());
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_event_not_approved", Name = "GetAllEventNotApproved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllEventNotApproved()
    {
        var result = await Sender.Send(new Query.GetAllEventNotApproved());
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    //[Authorize(Policy = "Admin")]
    [HttpPut("approved_by_admin", Name = "ApprovedEventByAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApprovedEventByAdmin([FromBody] Command.ApprovedEventByAdmin approveEvent)
    {
        var result = await Sender.Send(approveEvent);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}
