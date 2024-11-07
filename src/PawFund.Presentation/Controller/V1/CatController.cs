using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Cats;
using PawFund.Presentation.Abstractions;
using System.Security.Claims;
using static PawFund.Contract.Services.Cats.Filter;

namespace PawFund.Presentation.Controller.V1;
public class CatController : ApiController
{
    public CatController(ISender sender) : base(sender)
    {
    }

    [Authorize(Policy = "StaffPolicy")]
    [HttpPost("create_cat", Name = "CreateCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCat([FromForm] Command.CreateCatCommand request)
    {
        var userId = User.FindFirstValue("UserId");
        var result = await Sender.Send(new Command.CreateCatCommand(request.Sex, request.Name, request.Age, request.Breed, request.Weight, request.Color, request.Description, request.Sterilization, request.Images, Guid.Parse(userId)));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPut("update_cat", Name = "UpdateCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCat([FromForm] Command.UpdateCatCommand reqeust)
    {
        var result = await Sender.Send(reqeust);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
    
    [HttpDelete("delete_cat", Name = "DeleteCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCat([FromBody] Command.DeleteCatCommand request)
    {
        var result = await Sender.Send(request);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_cat_by_id", Name = "GetCatById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCatById([FromQuery] Guid Id)
    {
        var userId = User.FindFirstValue("UserId");
        var parsedUserId = string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);

        var result = await Sender.Send(new Query.GetCatByIdQuery(Id, parsedUserId));

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

    [Authorize(Policy = "StaffPolicy")]
    [HttpGet("get_cats_staff", Name = "GetCatsStaff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCatsStaff(
        [FromQuery] CatFilter filterParams,
    [FromQuery] int pageIndex = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string[] selectedColumns = null
        )
    {
        var userId = User.FindFirstValue("UserId");
        var filter = new CatFilter(filterParams.CatSex, Guid.Parse(userId), filterParams.Name, filterParams.Color, filterParams.Sterilization, filterParams.Age, filterParams.IsDeleted);
        var result = await Sender.Send(new Query.GetCatsStaffQuery(pageIndex, pageSize, filter, selectedColumns));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}

