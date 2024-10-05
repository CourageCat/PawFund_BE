using FluentValidation;

namespace PawFund.Contract.Services.Authentications.Validators;

internal class ForgotPasswordEmailValidator : AbstractValidator<Command.ForgotPasswordEmailCommand>
{
    public ForgotPasswordEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}

