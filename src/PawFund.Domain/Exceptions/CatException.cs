using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Domain.Exceptions;

public static class CatException
{

    public class CatNotFoundException : NotFoundException
    {
        public CatNotFoundException() : base(MessagesList.CatNotFoundException.GetMessage().Message,
               MessagesList.CatNotFoundException.GetMessage().Code)
        {
        }
    }

    public class UpdateCatFailException : BadRequestException
    {
        public UpdateCatFailException() : base(MessagesList.UpdateCatFail.GetMessage().Message,
               MessagesList.UpdateCatFail.GetMessage().Code)
        {
        }
    }
}
