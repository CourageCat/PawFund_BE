using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Domain.Exceptions
{
    public static class AdoptApplicationException
    {
        public class AdoptApplicationNotFoundException : NotFoundException
        {
            public AdoptApplicationNotFoundException(Guid Id) : base(string.Format(MessagesList.AdoptApplicationNotFoundException.GetMessage().Message, Id), MessagesList.AdoptApplicationNotFoundException.GetMessage().Code)
            { }
        }
        public class AdoptApplicationNotBelongToAdopterException : BadRequestException
        {
            public AdoptApplicationNotBelongToAdopterException() : base(MessagesList.AdoptApplicationNotBelongToAdopterException.GetMessage().Message, MessagesList.AdoptApplicationNotBelongToAdopterException.GetMessage().Code)
            { }
        }
        public class AdopterHasAlreadyRegisteredWithCatException : BadRequestException
        {
            public AdopterHasAlreadyRegisteredWithCatException() : base(MessagesList.AdoptAdopterHasAlreadyRegisteredWithCatException.GetMessage().Message, MessagesList.AdoptAdopterHasAlreadyRegisteredWithCatException.GetMessage().Code)
            { }
        }
        public class AdoptApplicationEmptyException : NotFoundException
        {
            public AdoptApplicationEmptyException() : base(MessagesList.AdoptApplicationEmptyException.GetMessage().Message, MessagesList.AdoptApplicationEmptyException.GetMessage().Code)
            { }
        }

        public class AdoptApplicationHasAlreadyApprovedException : BadRequestException
        {
            public AdoptApplicationHasAlreadyApprovedException() : base(MessagesList.AdoptApplicationHasAlreadyApprovedException.GetMessage().Message, MessagesList.AdoptApplicationHasAlreadyApprovedException.GetMessage().Code)
            { }
        }

        public class AdoptApplicationHasAlreadyRejectedException : BadRequestException
        {
            public AdoptApplicationHasAlreadyRejectedException() : base(MessagesList.AdoptApplicationHasAlreadyRejectedException.GetMessage().Message, MessagesList.AdoptApplicationHasAlreadyRejectedException.GetMessage().Code)
            { }
        }
    }
}
