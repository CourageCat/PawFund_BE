using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.HistoryCats;
using PawFund.Presentation.Abstractions;

namespace PawFund.Presentation.Controller.V1;
public class HistoryCatController : ApiController
{
    public HistoryCatController(ISender sender) : base(sender)
    {
    }

    [HttpPost("create_history_cat", Name = "CreateHistoryCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateHistoryCat([FromBody] Command.CreateHistoryCatCommand CreateHistoryCat)
    {
        var result = await Sender.Send(CreateHistoryCat);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPut("update_history_cat", Name = "UpdateHistoryCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHistoryCat([FromBody] Command.UpdateHistoryCatCommand UpdateHistoryCat)
    {
        var result = await Sender.Send(UpdateHistoryCat);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpDelete("delete_history_cat", Name = "DeleteHistoryCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHistoryCat([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Command.DeleteHistoryCatCommand(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_history_cat_by_id", Name = "GetHistoryCatById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHistoryCatById([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Query.GetHistoryCatByIdQuery(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}

