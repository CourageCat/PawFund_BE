using FluentValidation;
using PawFund.Contract.Services.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Admins.Validator
{
    public class GetDashboardValidator : AbstractValidator<Query.GetDashboardQuery>
    {
        public GetDashboardValidator()
        {
            RuleFor(x => x.Year).NotEmpty();
        }
    }
}
