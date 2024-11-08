using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;
public class UpdateMeetingTimeValidator : AbstractValidator<Command.UpdateMeetingTimeCommand>
{
    public UpdateMeetingTimeValidator()
    {
        var now = DateTime.Now;
        RuleFor(x => x.AccountId).NotEmpty();
        RuleForEach(x => x.ListTime)
        .ChildRules(listItem =>
        {
            listItem.RuleFor(item => item.MeetingTime)
                .GreaterThan(now)
                .WithMessage("Each time in the list must be strictly greater than the current time.");
            listItem.RuleFor(item => item.NumberOfStaffsFree)
                .GreaterThan(0)
                .WithMessage("Number Of Staff Free must be higher than 0");
        });
    }
}
