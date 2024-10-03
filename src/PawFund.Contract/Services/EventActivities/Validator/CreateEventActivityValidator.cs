using FluentValidation;

namespace PawFund.Contract.Services.EventActivities.Validator
{
    public class CreateEventActivityValidator : AbstractValidator<Command.CreateEventActivityCommand>
    {
        public CreateEventActivityValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.NumberOfVolunteer).NotEmpty();
            RuleFor(x => x.Time).NotEmpty();
            RuleFor(x => x.Desciption).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
        }
    }
}
