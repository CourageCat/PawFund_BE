using FluentValidation;

namespace PawFund.Contract.Services.AdoptApplications.Validators;
public class GetAllApplicationByAdopterValidator : AbstractValidator<Query.GetAllApplicationByAdopterQuery>
{
    public GetAllApplicationByAdopterValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
    }
}
