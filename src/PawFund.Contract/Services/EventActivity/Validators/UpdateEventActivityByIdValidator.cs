using FluentValidation;

namespace PawFund.Contract.Services.EventActivity.Validators
{
    class UpdateEventActivityValidator : AbstractValidator<Command.UpdateEventActivityCommand>
    {
        public UpdateEventActivityValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
        }

    }
}
