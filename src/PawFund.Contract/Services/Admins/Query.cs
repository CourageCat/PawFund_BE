using PawFund.Contract.Abstractions.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.Admin.Response;

namespace PawFund.Contract.Services.Admin
{
    public static class Query
    {
        public record GetAllUser() : IQuery<UserResponse>;
    }
}
