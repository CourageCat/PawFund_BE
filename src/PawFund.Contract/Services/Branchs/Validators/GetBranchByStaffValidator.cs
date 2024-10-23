using FluentValidation;

namespace PawFund.Contract.Services.Branchs.Validators;
public class GetBranchByStaffValidator : AbstractValidator<Query.GetBranchByStaffQuery>
{
    public GetBranchByStaffValidator()
    {
        RuleFor(x => x.StaffId).NotEmpty();
    }
}
