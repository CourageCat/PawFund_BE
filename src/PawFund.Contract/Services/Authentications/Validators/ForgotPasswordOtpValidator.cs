using FluentValidation;

namespace PawFund.Contract.Services.Authentications.Validators;

internal class ForgotPasswordOtpValidator : AbstractValidator<Command.ForgotPasswordOtpCommand>
{
    public ForgotPasswordOtpValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("Otp is required.")
            .Length(5).WithMessage("Required Otp length is 5.");
    }
}