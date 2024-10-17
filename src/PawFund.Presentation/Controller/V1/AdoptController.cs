using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.DTOs.Adopt.Request;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Presentation.Abstractions;


namespace PawFund.Presentation.Controller.V1;

public class AdoptController : ApiController
{
    public AdoptController(ISender sender) : base(sender)
    {
    }

    //[Authorize]
    [HttpPost("create_adopt_application", Name = "CreateAdoptApplication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAdoptApplication([FromBody] CreateAdoptApplicationRequestDTO CreateAdoptApplication)
    {
        //var accountId = Guid.Parse(User.FindFirstValue("UserId"));
        var accountId = Guid.Parse("3F2A04BD-EAB9-4058-8C8A-242DFAAA1082");
        var result = await Sender.Send(new Command.CreateAdoptApplicationCommand(CreateAdoptApplication.Description, accountId, CreateAdoptApplication.CatId));
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
    public async Task<IActionResult> GetAllAplication([FromQuery] int pageIndex = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] bool isAscCreatedDate = false,
    [FromQuery] string[] selectedColumns = null)
    {
        var result = await Sender.Send(new Query.GetAllApplicationQuery(pageIndex, pageSize, isAscCreatedDate, selectedColumns));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_application_by_adopter", Name = "GetAllApplicationByAdopter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApplicationByAdopter(
    [FromQuery] int pageIndex = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] bool isAscCreatedDate = false,
    [FromQuery] string[] selectedColumns = null)
    {
        //var accountId = Guid.Parse(User.FindFirstValue("UserId"));
        var accountId = Guid.Parse("3F2A04BD-EAB9-4058-8C8A-242DFAAA1082");
        var result = await Sender.Send(new Query.GetAllApplicationByAdopterQuery(accountId, pageIndex, pageSize, isAscCreatedDate, selectedColumns));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("get_all_application_by_staff", Name = "GetAllApplicationByStaff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApplicationOnCat()
    {
        //var accountId = Guid.Parse(User.FindFirstValue("UserId"));
        var accountId = Guid.Parse("F78FDAE8-6955-4404-9803-FC3EDF96D029");
        var result = await Sender.Send(new Query.GetApplicationByIdQuery(accountId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    //[Authorize]
    [HttpPut("update_meeting_time", Name = "UpdateMeetingTime")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMeetingTime([FromBody] List<UpdateMeetingTimeRequestDTO.MeetingTimeDTO> listMeetingTime)
    {
        //var accountId = Guid.Parse(User.FindFirstValue("UserId"));
        var accountId = Guid.Parse("F78FDAE8-6955-4404-9803-FC3EDF96D029");
        var result = await Sender.Send(new Command.UpdateMeetingTimeCommand(accountId, listMeetingTime));
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

    [HttpGet("get_meeting_time_by_adopter", Name = "GetMeetingTimeByAdopter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMeetingTimeByAdopter([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Query.GetMeetingTimeByAdopterQuery(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
    //[Authorize]
    [HttpGet("get_meeting_time_by_staff", Name = "GetMeetingTimeByStaff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMeetingTimeByStaff()
    {
        //var accountId = Guid.Parse(User.FindFirstValue("UserId"));
        var accountId = Guid.Parse("F78FDAE8-6955-4404-9803-FC3EDF96D029");
        var result = await Sender.Send(new Query.GetMeetingTimeByStaffQuery(accountId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

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

