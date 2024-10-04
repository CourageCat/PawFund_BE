using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Adopt;
using PawFund.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Presentation.Controller.V1;

public class AdoptController : ApiController
{
    public AdoptController(ISender sender) : base(sender)
    {
    }

    [HttpPost("create_adopt_application", Name = "CreateAdoptApplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAdoptApplication([FromBody] Command.CreateAdoptApplicationCommand CreateAdoptApplication)
    {
        var result = await Sender.Send(CreateAdoptApplication);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPut("update_adopt_application", Name = "UpdateAdoptApplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAdoptApplication([FromBody] Command.UpdateAdoptApplicationCommand UpdateAdoptApplication)
    {
        var result = await Sender.Send(UpdateAdoptApplication);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpDelete("delete_adopt_application_by_adopter", Name = "DeleteAdoptApplicationByAdopter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAdoptApplicationByAdopter([FromBody] Command.DeleteAdoptApplicationByAdopterCommand DeleteAdoptApplicationByAdopter)
    {
        var result = await Sender.Send(DeleteAdoptApplicationByAdopter);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_application_by_id", Name = "GetApplicationById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetApplicationById([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Query.GetApplicationByIdQuery(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_application", Name = "GetAllAplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAplication([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Query.GetApplicationByIdQuery(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_application_by_adopter", Name = "GetAllAplicationByAdopter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAplicationByAdopter([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Query.GetApplicationByIdQuery(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_application_on_cat", Name = "GetAllAplicationOnCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAplicationOnCat([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Query.GetApplicationByIdQuery(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}

