using FluentValidation;
using PawFund.Contract.Services.AdoptApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Cats.Validator
{
    public class GetCatByIdValidator : AbstractValidator<Query.GetCatByIdQuery>
    {
        public GetCatByIdValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }
}
