using PawFund.Contract.DTOs.PaymentDTOs;

namespace PawFund.Contract.Abstractions.Services;

public interface IPaymentService
{
    Task<CreatePaymentResponseDTO> CreatePaymentLink(CreatePaymentDTO paymentData);
}
