using FluentValidation;
using PawFund.Contract.Services.AdoptApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.AdoptApplications.Validator
{
    public class GetApplicationByIdValidator : AbstractValidator<Query.GetApplicationByIdQuery>
    {
        public GetApplicationByIdValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
