using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Donate;
using PawFund.Contract.Shared;

namespace PawFund.Application.UseCases.V1.Commands.Donate;

public sealed class CreateDonationBankingCommandHandler : ICommandHandler<Command.CreateDonationBankingCommand>
{
    //private readonly I
    public async Task<Result> Handle(Command.CreateDonationBankingCommand request, CancellationToken cancellationToken)
    {
        
        throw new NotImplementedException();
    }
}
