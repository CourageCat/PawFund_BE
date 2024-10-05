using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.AdoptApplications.Validators
{
    public class DeleteAdoptApplicationByAdopterValidator : AbstractValidator<Command.DeleteAdoptApplicationByAdopterCommand>
    {
        public DeleteAdoptApplicationByAdopterValidator()
        {
            RuleFor(x => x.AdoptId).NotEmpty();
        }
    }
}
