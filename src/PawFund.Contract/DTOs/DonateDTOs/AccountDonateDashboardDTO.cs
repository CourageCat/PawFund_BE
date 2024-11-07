namespace PawFund.Contract.DTOs.DonateDTOs;

public class AccountDonateDashboardDTO
{
    public AccountDonateDashboardDTO()
    {
    }

    public AccountDonateDashboardDTO(string imageUrl, string email, double amount, double percentage)
    {
        ImageUrl = imageUrl;
        Email = email;
        Amount = amount;
        Percentage = percentage;
    }

    public string ImageUrl { get; set; }
    public string Email { get; set; }
    public double Amount { get; set; }
    public double Percentage { get; set; }
}
