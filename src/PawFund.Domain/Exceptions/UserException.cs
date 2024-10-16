using PawFund.Contract.Enumarations.MessagesList;
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

        public class UserHasAlreadyBannedException : BadRequestException
        {
            public UserHasAlreadyBannedException() : base(string.Format(MessagesList.UserHasAlreadyBannedException.GetMessage().Message), MessagesList.UserHasAlreadyBannedException.GetMessage().Code)
            { }
        }

        public class UserHasAlreadyUnbannedException : BadRequestException
        {
            public UserHasAlreadyUnbannedException() : base(string.Format(MessagesList.UserHasAlreadyUnbannedException.GetMessage().Message), MessagesList.UserHasAlreadyUnbannedException.GetMessage().Code)
            { }
        }
    }
}
