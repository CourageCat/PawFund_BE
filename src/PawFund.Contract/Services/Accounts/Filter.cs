using PawFund.Contract.Enumarations.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Accounts
{
    public static class Filter
    {
        public record AccountFilter(string? FirstName, bool? Status, RoleType? RoleType);

    }
}
