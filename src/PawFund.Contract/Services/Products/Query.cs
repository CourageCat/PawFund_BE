using PawFund.Contract.Abstractions.Message;
using static PawFund.Contract.Services.Products.Response;

namespace PawFund.Contract.Services.Products;

public static class Query
{
    public record GetProductById(Guid Id) : IQuery<ProductResponse>;
}
