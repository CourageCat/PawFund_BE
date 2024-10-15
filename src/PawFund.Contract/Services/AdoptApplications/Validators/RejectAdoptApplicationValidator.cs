using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;

public class RejectAdoptApplicationValidator : AbstractValidator<Command.RejectAdoptApplicationCommand>
{
    public RejectAdoptApplicationValidator()
    {
        RuleFor(x => x.AdoptId).NotEmpty();
        RuleFor(x => x.ReasonReject).NotEmpty().MaximumLength(50);
    }
}
