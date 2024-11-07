using PawFund.Contract.DTOs.DonateDTOs;
using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Contract.Services.Admin;

public static class Response
{
    public record UsersDonateResponse();
    public record DashboardResponse(int TotalCats, int TotalAdoptApplications, int TotalEvents, double TotalDonations, int TotalVolunteerApplications, int TotalUsers, List<string> ListMonths, List<double> ListDonationInYear, List<AccountDonateDashboardDTO> ListFiveUsersDonated);
}
