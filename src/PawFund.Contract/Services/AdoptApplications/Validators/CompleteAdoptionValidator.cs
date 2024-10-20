using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;

public class CompleteAdoptionValidator : AbstractValidator<Command.CompleteAdoptionCommand>
{
    public CompleteAdoptionValidator()
    {
        RuleFor(x => x.AdoptId).NotEmpty();
    }
}
