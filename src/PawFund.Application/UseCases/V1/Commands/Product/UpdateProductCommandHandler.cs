using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Products;
using PawFund.Contract.Shared;

namespace PawFund.Application.UseCases.V1.Commands.Product;

public sealed class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProductCommand>
{
    public Task<Result> Handle(Command.UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
