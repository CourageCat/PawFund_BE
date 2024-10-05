using PawFund.Contract.Abstractions.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.Accounts.Response;

namespace PawFund.Contract.Services.Accounts
{
    public static class Query
    {
        public record GetUserById(Guid Id) : IQuery<UserResponse>;

        public record GetListUser() : IQuery<UserResponse>;
    }
}
