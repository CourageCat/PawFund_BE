using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.EventActivity.Validators
{
    class CreateEventActivityValidator : AbstractValidator<Command.CreateEventActivityCommand>
    {
        public CreateEventActivityValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
        }

    }
}
