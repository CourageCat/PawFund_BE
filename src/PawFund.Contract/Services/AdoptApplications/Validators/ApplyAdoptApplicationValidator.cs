using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;
public class ApplyAdoptApplicationValidator : AbstractValidator<Command.ApplyAdoptApplicationCommand>
{
    public ApplyAdoptApplicationValidator()
    {
        RuleFor(x => x.AdoptId).NotNull().NotEmpty();
    }
}
