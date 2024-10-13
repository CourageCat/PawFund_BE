using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Cats.Validators
{
    public class UpdateCatValidator : AbstractValidator<Command.UpdateCatCommand>
    {
        public UpdateCatValidator()
        {
            RuleFor(x => x.Id).NotEmpty();  
            RuleFor(x => x.Sex).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Age).NotEmpty();
            RuleFor(x => x.Breed).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
