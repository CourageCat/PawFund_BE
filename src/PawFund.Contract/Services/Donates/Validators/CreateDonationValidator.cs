using FluentValidation;

namespace PawFund.Contract.Services.Donate.Validators
{
    public class CreateDonationValidator : AbstractValidator<Command.CreateDonationCommand>
    {
        public CreateDonationValidator()
        {
            RuleFor(x => x.amount).NotEmpty();
        }
    }
}
