using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class UserException
    {
        public sealed class UserNotFoundException : NotFoundException
        {
            public UserNotFoundException(Guid Id)
                : base($"User with id:{Id} was not found !") { }
        }

        public sealed class ListUserNotFoundException : NotFoundException
        {
            public ListUserNotFoundException()
                : base($"Do not have any user in list !") { }
        }
    }
}
