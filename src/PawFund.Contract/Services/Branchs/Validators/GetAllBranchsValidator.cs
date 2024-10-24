using FluentValidation;

namespace PawFund.Contract.Services.Branchs.Validators;

public class GetAllBranchsValidator : AbstractValidator<Query.GetAllBranchesQuery>
{
    public GetAllBranchsValidator()
    {

    }
}
