using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.AdoptApplications;
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
    public async Task<IActionResult> DeleteAdoptApplicationByAdopter([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Command.DeleteAdoptApplicationByAdopterCommand(Id));
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
    public async Task<IActionResult> GetAllAplication()
    {
        var result = await Sender.Send(new Query.GetAllApplicationQuery());
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_application_by_adopter", Name = "GetAllApplicationByAdopter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApplicationByAdopter([FromQuery] Guid AccountId)
    {
        var result = await Sender.Send(new Query.GetApplicationByIdQuery(AccountId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_application_on_cat", Name = "GetAllApplicationOnCat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApplicationOnCat([FromQuery] Guid CatId)
    {
        var result = await Sender.Send(new Query.GetApplicationByIdQuery(CatId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPut("update_meeting_time", Name = "UpdateMeetingTime")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMeetingTime([FromBody] Command.UpdateMeetingTimeCommand UpdateMeetingTime)
    {
        var result = await Sender.Send(UpdateMeetingTime);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPut("apply_adopt_application", Name = "ApplyAdoptApplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApplyAdoptApplication([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Command.ApplyAdoptApplicationCommand(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPut("reject_adopt_application", Name = "RejectAdoptApplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RejectAdoptApplication([FromBody] Command.RejectAdoptApplicationCommand RejectAdoptApplication)
    {
        var result = await Sender.Send(RejectAdoptApplication);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    //[HttpPut("get_meeting_time_by_adopter", Name = "GetMeetingTimeByAdopter")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetMeetingTimeByAdopter([FromQuery] Guid Id)
    //{
    //    var result = await Sender.Send(new Command.GetMeetingTimeByAdopterQuery(Id));
    //    if (result.IsFailure)
    //        return HandlerFailure(result);

    //    return Ok(result);
    //}

    //[HttpPut("get_meeting_time_by_staff", Name = "GetMeetingTimeByStaff")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetMeetingTimeByStaff()
    //{
    //    var result = await Sender.Send(new Command.GetMeetingTimeByStaffQuery(Id));
    //    if (result.IsFailure)
    //        return HandlerFailure(result);

    //    return Ok(result);
    //}

    //[HttpPut("choose_meeting_time", Name = "ChooseMeetingTime")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> ChooseMeetingTime()
    //{
    //    var result = await Sender.Send(new Command.ChooseMeetingTimeCommand(Id));
    //    if (result.IsFailure)
    //        return HandlerFailure(result);

    //    return Ok(result);
    //}

    //[HttpPut("complete_adoption", Name = "CompleteAdoption")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> CompleteAdoption()
    //{
    //    var result = await Sender.Send(new Command.CompleteAdoptionCommand(Id));
    //    if (result.IsFailure)
    //        return HandlerFailure(result);

    //    return Ok(result);
    //}

    //[HttpPut("reject_outside", Name = "RejectOutside")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> RejectOutside()
    //{
    //    var result = await Sender.Send(new Command.RejectOutsideCommand(Id));
    //    if (result.IsFailure)
    //        return HandlerFailure(result);

    //    return Ok(result);
    //}

    //[HttpPost("update_data_from_google_sheet", Name = "UpdateDataFromGoogleSheet")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UpdateDataFromGoogleSheet([FromBody] Command.UpdateDataFromGoogleSheetCommand UpdateDataFromGoogleSheet)
    //{
    //    var result = await Sender.Send(UpdateDataFromGoogleSheet);
    //    if (result.IsFailure)
    //        return HandlerFailure(result);

    //    return Ok(result);
    //}
}

