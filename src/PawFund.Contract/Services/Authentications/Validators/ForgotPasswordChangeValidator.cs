using FluentValidation;

namespace PawFund.Contract.Services.Authentications.Validators;

internal class ForgotPasswordChangeValidator : AbstractValidator<Command.ForgotPasswordChangeCommand>
{
    public ForgotPasswordChangeValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");

        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("Otp is required.")
            .Length(5).WithMessage("Required Otp length is 5.");
    }
}