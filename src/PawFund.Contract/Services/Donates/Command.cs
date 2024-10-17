namespace PawFund.Contract.Services.Donate;

public static class Command
{
    public record CreateDonationCommand(decimal amount, string description, Guid PaymentMethodId) : Abstractions.Message.ICommand;
    public record CreateDonationBankingCommand(Guid AccountId, int Amount, string Description): Abstractions.Message.ICommand;
}