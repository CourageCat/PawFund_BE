using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Donors.Validators
{
    public class CreateDonationValidator : AbstractValidator<Command.CreateDonationCommand>
    {
        public CreateDonationValidator()
        {
            RuleFor(x => x.amount).NotEmpty();
        }
    }
}
