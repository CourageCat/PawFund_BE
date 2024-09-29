using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Products;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Repositories;

namespace PawFund.Application.UseCases.V1.Commands.Product;

public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand>
{
    private readonly IRepositoryBase<PawFund.Domain.Entities.Product, Guid> _productRepository;
    private readonly IEFUnitOfWork _unitOfWork;

    public CreateProductCommandHandler
        (IRepositoryBase<Domain.Entities.Product, Guid> productRepository, IEFUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        _productRepository.Add(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
