using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class AccountException
    {
        public class AccountNotFoundException : NotFoundException
        {
            public AccountNotFoundException(Guid Id) : base($"Can not found Account with Id: {Id}")
            {
            }
        }
    }
}
