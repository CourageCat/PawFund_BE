using PawFund.Contract.MessagesList;

namespace PawFund.Domain.Exceptions;

public static class AuthenticationException
{
    public sealed class EmailExistException : BadRequestException
    {
        public EmailExistException()
            : base($"This email has been registered") { }
    }

    public sealed class EmailNotFoundException : NotFoundException
    {
        public EmailNotFoundException()
           : base(MessagesList.AuthEmailNotFoundException.GetMessage().Message,
                 MessagesList.AuthEmailNotFoundException.GetMessage().Code) { }
    }

    public sealed class UserNotFoundException : BadRequestException
    {
        public UserNotFoundException()
            : base($"Registration time has passed, please register again.") { }
    }

    public sealed class PasswordNotMatchException : BadRequestException
    {
        public PasswordNotMatchException() 
            : base($"Passwords do not match") { }
    }

    public sealed class RefreshTokenNull : AuthorizeException
    {
        public RefreshTokenNull()
           : base($"Het han dang nhap, xin vui long dang nhap lai") { }
    }
    public sealed class UserNotFoundByIdException : NotFoundException 
    { 
        public UserNotFoundByIdException(Guid Id) :
            base($"Can not found User with Id: {Id}") { }
    }
    public sealed class OtpForgotPasswordNotMatch : NotFoundException
    {
        public OtpForgotPasswordNotMatch() :
            base($"Your otp does not match")
        { }
    }

    public sealed class ErrorChangePassword : BadRequestException
    {
        public ErrorChangePassword() :
           base($"An error occurred, please try again")
        { }
    }
}
