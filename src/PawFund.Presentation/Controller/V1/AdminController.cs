﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Admin;
using PawFund.Presentation.Abstractions;
using static PawFund.Contract.Services.Accounts.Filter;
using static PawFund.Contract.Services.Donates.Filter;



namespace PawFund.Presentation.Controller.V1
{
    public class AdminController : ApiController
    {
        public AdminController(ISender sender) : base(sender)
        {
        }

        [HttpPost("ban_user", Name = "BanUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BanUserById([FromBody] Contract.Services.Admins.Command.BanUserCommand ChangeStatus)
        {
            var result = await Sender.Send(ChangeStatus);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpPost("unban_user", Name = "UnbanUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnbanUserById([FromBody] Contract.Services.Admins.Command.UnBanUserCommand ChangeStatus)
        {
            var result = await Sender.Send(ChangeStatus);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_list_user", Name = "GetListUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers([FromQuery] AccountsFilter filterParams,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string[] selectedColumns = null)
        {
            var result = await Sender.Send(new Contract.Services.Accounts.Query.GetUsersQueryHandler(pageIndex, pageSize, filterParams, selectedColumns));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_list_users_donate", Name = "GetListUserDonateAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersDonate([FromQuery] DonateFilter filterParams,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string[] selectedColumns = null)
        {
            var result = await Sender.Send(new Contract.Services.Donate.Query.GetDonatesQuery(pageIndex, pageSize, filterParams, selectedColumns));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_dashboard", Name = "GetDashboard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDashboard([FromQuery] Query.GetDashboardQuery getDashboardQuery)
        {
            var result = await Sender.Send(getDashboardQuery);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_list_user_by_year", Name = "GetListUserByYearAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByYear([FromQuery] int year)
        {
            var result = await Sender.Send(new Query.GetUsersByYearQuery(year));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_list_user_by_year_and_month", Name = "GetListUserByYearAndMonthAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByYearAndMonth([FromQuery] int year, [FromQuery] int month)
        {
            var result = await Sender.Send(new Query.GetUsersByYearAndMonthQuery(year, month));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

    }
}
