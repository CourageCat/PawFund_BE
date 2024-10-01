
using System.Windows.Input;
using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Admins
{
    public static class Command
    {
       public record BanUserCommand(Guid Id) : Abstractions.Message.ICommand;

       public record UnBanUserCommand(Guid Id);
    }
}
