namespace PawFund.Domain.Exceptions;

public abstract class AuthorizeException : DomainException
{
    protected AuthorizeException(string message)
        : base("Unauthorized", message)
    {
    }
}
