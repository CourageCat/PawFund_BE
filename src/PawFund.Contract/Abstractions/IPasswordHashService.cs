namespace PawFund.Contract.Abstractions;

public interface IPasswordHashService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHashed);
}
