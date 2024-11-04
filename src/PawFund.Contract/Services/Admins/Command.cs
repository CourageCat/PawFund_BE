namespace PawFund.Contract.Services.Admins;

public static class Command
{
   public record BanUserCommand(Guid Id, string Reason) : Abstractions.Message.ICommand;

   public record UnBanUserCommand(Guid Id) : Abstractions.Message.ICommand;

}
