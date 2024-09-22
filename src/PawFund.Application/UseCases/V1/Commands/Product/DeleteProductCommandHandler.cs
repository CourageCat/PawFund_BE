using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Products;
using PawFund.Contract.Shared;

namespace PawFund.Application.UseCases.V1.Commands.Product;

public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    public Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
