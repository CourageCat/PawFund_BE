using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.DTOs.PaymentDTOs;
using PawFund.Contract.Services.Products;
using PawFund.Contract.Shared;

namespace PawFund.Application.UseCases.V1.Queries.Product;

public class GetPaymentProductQueryHandler : IQueryHandler<Query.GetPaymentProductQueryHandler, CreatePaymentResponseDTO>
{
    private readonly IPaymentService _paymentService;

    public GetPaymentProductQueryHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<Result<CreatePaymentResponseDTO>> Handle(Query.GetPaymentProductQueryHandler request, CancellationToken cancellationToken)
    {
        var response = await _paymentService.CreatePaymentLink(request.paymentDto);
        return response;
    }
}
