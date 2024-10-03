using FluentValidation;
using PawFund.Contract.Services.Adopt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Adopts.Validator
{
    public class GetApplicationByIdValidator : AbstractValidator<Query.GetApplicationByIdQuery>
    {
        public GetApplicationByIdValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
