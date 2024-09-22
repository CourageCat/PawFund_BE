using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.Products;
public static class Command
{
    public record CreateProductCommand(string Name, decimal Price, string Description) : ICommand;
    public record UpdateProductCommand(Guid Id, string Name, decimal Price, string Description) : ICommand;
    public record DeleteProductCommand(Guid Id) : ICommand;
}

