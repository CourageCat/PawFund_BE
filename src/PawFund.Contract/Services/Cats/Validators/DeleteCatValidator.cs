using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Cats.Validators
{
    public class DeleteCatValidator : AbstractValidator<Command.DeleteCatCommand>
    {
        public DeleteCatValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
