using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Admins.Validator
{
    public class BanUserValidator : AbstractValidator<Command.ChangeUserStatusCommand>
    {
        public BanUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
