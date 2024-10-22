using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;
public class ChooseMeetingTimeValidator : AbstractValidator<Command.ChooseMeetingTimeCommand>
{
    public ChooseMeetingTimeValidator()
    {
        RuleFor(x => x.AdoptId).NotEmpty();
        RuleFor(x => x.MeetingTime).NotEmpty();
    }
}
