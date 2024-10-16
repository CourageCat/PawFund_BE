using PawFund.Contract.Enumarations.MessagesList;

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

    public sealed class RefreshTokenNullException : AuthorizeException
    {
        public RefreshTokenNullException()
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

    public sealed class OtpForgotPasswordNotMatchException : NotFoundException
    {
        public OtpForgotPasswordNotMatchException()
            : base(MessagesList.AuthOtpForgotPasswordNotMatchException.GetMessage().Message,
                   MessagesList.AuthOtpForgotPasswordNotMatchException.GetMessage().Code)
        { }
    }

    public sealed class ErrorChangePasswordException : BadRequestException
    {
        public ErrorChangePasswordException()
           : base(MessagesList.AuthErrorChangePasswordException.GetMessage().Message,
                  MessagesList.AuthErrorChangePasswordException.GetMessage().Code)
        { }
    }

    public sealed class EmailGoogleRegistedException : BadRequestException
    {
        public EmailGoogleRegistedException()
           : base(MessagesList.AuthGoogleEmailRegisted.GetMessage().Message,
                  MessagesList.AuthGoogleEmailRegisted.GetMessage().Code)
        { }
    }

    public sealed class LoginGoogleFailException : BadRequestException
    {
        public LoginGoogleFailException()
           : base(MessagesList.AuthLoginGoogleFail.GetMessage().Message,
                  MessagesList.AuthLoginGoogleFail.GetMessage().Code)
        { }
    }
    public sealed class AccountRegisteredAnotherMethodException : BadRequestException
    {
        public AccountRegisteredAnotherMethodException()
           : base(MessagesList.AuthAccountRegisteredAnotherMethod.GetMessage().Message,
                  MessagesList.AuthAccountRegisteredAnotherMethod.GetMessage().Code)
        { }
    }
}
