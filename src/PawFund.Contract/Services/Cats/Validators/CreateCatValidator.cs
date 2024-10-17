using FluentValidation;

namespace PawFund.Contract.Services.Cats.Validators
{
    public class CreateCatValidator : AbstractValidator<Command.CreateCatCommand>
    {
        public CreateCatValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Age).NotEmpty();
            RuleFor(x => x.Breed).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
        }
    }
}
