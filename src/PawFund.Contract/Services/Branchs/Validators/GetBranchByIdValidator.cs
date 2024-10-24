using FluentValidation;

namespace PawFund.Contract.Services.Branchs.Validators;
public class GetBranchByIdValidator : AbstractValidator<Query.GetBranchByIdQuery>
{
    public GetBranchByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
