using FluentValidation;

namespace PawFund.Contract.Services.Cats.Validator
{
    public class GetCatByIdValidator : AbstractValidator<Query.GetCatByIdQuery>
    {
        public GetCatByIdValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
