using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;
public class GetMeetingTimeByAdopterValidator : AbstractValidator<Query.GetMeetingTimeByAdopterQuery>
{
    public GetMeetingTimeByAdopterValidator()
    {
        RuleFor(x => x.AdoptId).NotEmpty();
    }

}
