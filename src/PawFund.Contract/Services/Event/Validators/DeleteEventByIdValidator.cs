using FluentValidation;
using PawFund.Contract.Services.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Events.Validator
{
    public class DeleteEventByIdValidator : AbstractValidator<Command.DeleteEventCommand>
    {
        public DeleteEventByIdValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
