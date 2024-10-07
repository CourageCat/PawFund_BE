using FluentValidation;
using PawFund.Contract.Services.Event;

namespace PawFund.Contract.Services.Events.Validator
{
    class GetEventByUdValidator : AbstractValidator<Query.GetEventByIdQuery>
    {
        public GetEventByUdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
