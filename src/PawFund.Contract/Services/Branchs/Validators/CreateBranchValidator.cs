using FluentValidation;

namespace PawFund.Contract.Services.Branchs.Validators;
public class CreateBranchValidator : AbstractValidator<Command.CreateBranchCommand>
{
    public CreateBranchValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.PhoneNumberOfBranch).NotEmpty();
        RuleFor(x => x.EmailOfBranch).EmailAddress();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.NumberHome).NotEmpty();
        RuleFor(x => x.StreetName).NotEmpty();
        RuleFor(x => x.Ward).NotEmpty();
        RuleFor(x => x.District).NotEmpty();
        RuleFor(x => x.Province).NotEmpty();
        RuleFor(x => x.PostalCode).NotEmpty();
        RuleFor(x => x.Image).NotEmpty();
    }
}
