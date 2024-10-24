using FluentValidation;

namespace PawFund.Contract.Services.Branchs.Validators;
public class DeleteBranchValidator : AbstractValidator<Command.DeleteBranchCommand>
{
    public DeleteBranchValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
