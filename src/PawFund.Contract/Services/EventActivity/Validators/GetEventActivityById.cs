using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.EventActivity.Validators
{
    class GetEventActivityByUdValidator : AbstractValidator<Query.GetEventActivityByIdQuery>
    {
        public GetEventActivityByUdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}

