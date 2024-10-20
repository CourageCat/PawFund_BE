using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;
public class RejectOutsideAdoptionValidator : AbstractValidator<Command.RejectOutsideAdoptionCommand>
{
    public RejectOutsideAdoptionValidator()
    {
        RuleFor(x => x.AdoptId).NotEmpty();
        RuleFor(x => x.ReasonReject).NotEmpty().MaximumLength(50);
    }
}
