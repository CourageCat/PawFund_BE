using FluentValidation;

namespace PawFund.Contract.Services.Cats.Validators;

public class UpdateCatValidator : AbstractValidator<Command.UpdateCatCommand>
{
    public UpdateCatValidator()
    {
        RuleFor(x => x.Sex).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Age).NotEmpty();
        RuleFor(x => x.Breed).NotEmpty();
        RuleFor(x => x.Weight).NotEmpty();
        RuleFor(x => x.Color).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
