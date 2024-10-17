namespace PawFund.Contract.Services.Donate;

public static class Command
{
    public record CreateDonationCommand(decimal amount, string description, Guid PaymentMethodId) : Abstractions.Message.ICommand;
    public record CreateDonationBankingCommand(Guid accountId, int amount, string description, Guid PaymentMethodId): Abstractions.Message.ICommand;
}
