
using System.Windows.Input;
using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Admins
{
    public static class Command
    {
       public record ChangeUserStatusCommand(Guid Id) : Abstractions.Message.ICommand;
    }
}
