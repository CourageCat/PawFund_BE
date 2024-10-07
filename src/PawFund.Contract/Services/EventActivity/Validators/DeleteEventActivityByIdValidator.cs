using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.EventActivity.Validators
{
    class DeleteEventActivityValidator : AbstractValidator<Command.DeleteEventActivityCommand>
    {
        public DeleteEventActivityValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
