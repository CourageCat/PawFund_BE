using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.EventActivity.Validators
{
    class GetAllEventActivityValidator : AbstractValidator<Query.GetAllEventActivity>
    {
        public GetAllEventActivityValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
