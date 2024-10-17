using PawFund.Contract.Enumarations.AdoptPetApplication;

namespace PawFund.Contract.Services.AdoptApplications;

public static class Filter
{
    public record AdoptApplicationFilter(AdoptPetApplicationStatus? Status, bool IsAscCreatedDate);
}
