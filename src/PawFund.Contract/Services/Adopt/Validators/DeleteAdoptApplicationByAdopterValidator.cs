using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Adopt.Validators
{
    public class DeleteAdoptApplicationByAdopterValidator : AbstractValidator<Command.DeleteAdoptApplicationByAdopterCommand>
    {
        public DeleteAdoptApplicationByAdopterValidator()
        {
            RuleFor(x => x.AdoptId).NotEmpty();
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }
}
