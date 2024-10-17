using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.PaymentDTOs;
using static PawFund.Contract.Services.Products.Response;

namespace PawFund.Contract.Services.Products;

public static class Query
{
    public record GetProductById(Guid Id) : IQuery<ProductResponse>;
    public record GetProductsPaginQueryHandler(int PageIndex,
        int PageSize,
        Filter.ProductFilter FilterParams,
        string[] SelectedColumns) 
        : IQuery<PagedResult<ProductResponse>>;

    public record GetPaymentProductQueryHandler(
        CreatePaymentDTO paymentDto) : IQuery<CreatePaymentResponseDTO>;
}
