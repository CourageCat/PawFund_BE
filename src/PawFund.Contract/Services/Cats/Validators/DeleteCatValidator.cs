using FluentValidation;

namespace PawFund.Contract.Services.Cats.Validators
{
    public class DeleteCatValidator : AbstractValidator<Command.DeleteCatCommand>
    {
        public DeleteCatValidator()
        {
        }
    }
}
