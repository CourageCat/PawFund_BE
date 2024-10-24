using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Branchs;
using PawFund.Presentation.Abstractions;
using System.Security.Claims;
using static PawFund.Contract.Services.Branchs.Filter;

namespace PawFund.Presentation.Controller.V1;
public class BranchController : ApiController
{
    public BranchController(ISender sender) : base(sender)
    {
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpPost("create_branch", Name = "CreateBranch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateBranch([FromBody] Command.CreateBranchCommand CreateBranch)
    {
        var result = await Sender.Send(CreateBranch);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpPut("update_branch", Name = "UpdateBranch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBranch([FromBody] Command.UpdateBranchCommand UpdateBranch)
    {
        var result = await Sender.Send(UpdateBranch);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpDelete("delete_branch", Name = "DeleteBranch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBranch([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Command.DeleteBranchCommand(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpGet("get_branch_by_id", Name = "GetBranchById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranchById([FromQuery] Guid Id)
    {
        var result = await Sender.Send(new Query.GetBranchByIdQuery(Id));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize(Policy = "StaffPolicy")]
    [HttpGet("get_branch_by_staff", Name = "GetBranchByStaff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranchByStaff()
    {
        var staffId = Guid.Parse(User.FindFirstValue("UserId"));
        var result = await Sender.Send(new Query.GetBranchByStaffQuery(staffId));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpGet("get_all_branches", Name = "GetAllBranches")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBranches(
        [FromQuery] BranchFilter filterParams,
    [FromQuery] int pageIndex = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string[] selectedColumns = null)
    {
        var result = await Sender.Send(new Query.GetAllBranchesQuery(pageIndex, pageSize, filterParams, selectedColumns));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}

