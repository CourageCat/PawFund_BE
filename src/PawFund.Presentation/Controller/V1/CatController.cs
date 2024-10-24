using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Cats;
using PawFund.Presentation.Abstractions;
using static PawFund.Contract.Services.Cats.Filter;
using static PawFund.Contract.Services.Products.Filter;

namespace PawFund.Presentation.Controller.V1;
public class CatController : ApiController
{
    public CatController(ISender sender) : base(sender)
    {
    }

    [HttpPost("create_cat", Name = "CreateCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCat([FromForm] Command.CreateCatCommand CreateCat)
    {
        var result = await Sender.Send(CreateCat);
        if (result.IsFailure)
            return HandlerFailure(result);
        
        return Ok(result);
    }

    [HttpPut("update_cat", Name = "UpdateCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCat([FromBody] Command.UpdateCatCommand UpdateCat)
    {
        var result = await Sender.Send(UpdateCat);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpDelete("delete_cat", Name = "DeleteCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCat([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Command.DeleteCatCommand(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_cat_by_id", Name = "GetCatById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCatById([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Query.GetCatByIdQuery(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_cats", Name = "GetCats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCats(
        [FromQuery] CatAdoptFilter filterParams,
    [FromQuery] int pageIndex = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string[] selectedColumns = null
        )
    {
        var result = await Sender.Send(new Query.GetCats(pageIndex, pageSize, filterParams, selectedColumns));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}

