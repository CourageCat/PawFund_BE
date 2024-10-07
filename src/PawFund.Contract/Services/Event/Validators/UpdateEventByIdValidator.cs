using FluentValidation;
using PawFund.Contract.Services.Event;

namespace PawFund.Contract.Services.Events.Validator
{
    public class UpdateEventByIdValidator : AbstractValidator<Command.UpdateEventCommand>
    {
        public UpdateEventByIdValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.MaxAttendees).NotEmpty().GreaterThan(1);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
        }
    }
}
