using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Domain.Exceptions;

public static class AccountException
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException()
        : base(MessagesList.AccountNotFound.GetMessage().Message,
               MessagesList.AccountNotFound.GetMessage().Code)
        { }
    }

    public class AccountEmailDuplicateException : BadRequestException
    {
        public AccountEmailDuplicateException()
      : base(MessagesList.AccountEmailDuplicate.GetMessage().Message,
             MessagesList.AccountEmailDuplicate.GetMessage().Code)
        { }
    }
}
