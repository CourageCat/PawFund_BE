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
           : base($"This email has not been registered, please try again") { }
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
}
