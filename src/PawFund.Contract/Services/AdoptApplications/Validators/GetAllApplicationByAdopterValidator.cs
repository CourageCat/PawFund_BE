using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;
public class GetAllApplicationByStaffValidator : AbstractValidator<Query.GetAllApplicationByStaffQuery>
{
    public GetAllApplicationByStaffValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
    }
}
