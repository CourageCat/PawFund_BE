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
}
