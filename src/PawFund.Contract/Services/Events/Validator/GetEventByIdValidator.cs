using FluentValidation;

namespace PawFund.Contract.Services.Events.Validator
{
    class GetEventByUdValidator : AbstractValidator<Query.GetEventById>
    {
        public GetEventByUdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
