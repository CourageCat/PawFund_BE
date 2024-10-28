using PawFund.Contract.DTOs.Account;
using PawFund.Contract.Enumarations.PaymentMethod;

namespace PawFund.Contract.DTOs.DonateDTOs;

public class DonateDto
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string? Description { get; set; }
    public PaymentMethodType PaymentMethodId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public AccountDto? Account { get; set; }
}


