using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Adopt.Validators
{
    public class CreateAdoptApplicationValidator : AbstractValidator<Command.CreateAdoptApplicationCommand>
    {
        public CreateAdoptApplicationValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.CatId).NotEmpty();
        }
    }
}
