using FluentValidation;
namespace PawFund.Contract.Services.AdoptApplications.Validators;

public class GetMeetingTimeByStaffValidator : AbstractValidator<Query.GetMeetingTimeByStaffQuery>
{
    public GetMeetingTimeByStaffValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
    }
}
