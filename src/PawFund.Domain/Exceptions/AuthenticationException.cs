using PawFund.Contract.MessagesList;

namespace PawFund.Domain.Exceptions;

public static class AuthenticationException
{
    public sealed class EmailExistException : BadRequestException
    {
        public EmailExistException()
            : base(MessagesList.AuthEmailExistException.GetMessage().Message,
                   MessagesList.AuthEmailExistException.GetMessage().Code)
        { }
    }

    public sealed class EmailNotFoundException : NotFoundException
    {
        public EmailNotFoundException()
           : base(MessagesList.AuthEmailNotFoundException.GetMessage().Message,
                  MessagesList.AuthEmailNotFoundException.GetMessage().Code)
        { }
    }

    public sealed class UserNotFoundException : BadRequestException
    {
        public UserNotFoundException()
            : base(MessagesList.AuthUserNotFoundException.GetMessage().Message,
                   MessagesList.AuthUserNotFoundException.GetMessage().Code)
        { }
    }

    public sealed class PasswordNotMatchException : BadRequestException
    {
        public PasswordNotMatchException()
            : base(MessagesList.AuthPasswordNotMatchException.GetMessage().Message,
                   MessagesList.AuthPasswordNotMatchException.GetMessage().Code)
        { }
    }

    public sealed class RefreshTokenNull : AuthorizeException
    {
        public RefreshTokenNull()
           : base(MessagesList.AuthRefreshTokenNull.GetMessage().Message,
                  MessagesList.AuthRefreshTokenNull.GetMessage().Code)
        { }
    }

    public sealed class UserNotFoundByIdException : NotFoundException
    {
        public UserNotFoundByIdException(Guid Id)
            : base(string.Format(MessagesList.AuthUserNotFoundByIdException.GetMessage().Message, Id),
                   MessagesList.AuthUserNotFoundByIdException.GetMessage().Code)
        { }
    }

    public sealed class OtpForgotPasswordNotMatch : NotFoundException
    {
        public OtpForgotPasswordNotMatch()
            : base(MessagesList.AuthOtpForgotPasswordNotMatchException.GetMessage().Message,
                   MessagesList.AuthOtpForgotPasswordNotMatchException.GetMessage().Code)
        { }
    }

    public sealed class ErrorChangePassword : BadRequestException
    {
        public ErrorChangePassword()
           : base(MessagesList.AuthErrorChangePasswordException.GetMessage().Message,
                  MessagesList.AuthErrorChangePasswordException.GetMessage().Code)
        { }
    }
}
