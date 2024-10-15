using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.AdoptApplications;
public static class DomainEvent
{
    public record AdopterHasBeenApproved(Guid Id, string Email, string CatName) : IDomainEvent;
    public record AdopterHasBeenRejected(Guid Id, string Email, string CatName, string ReasonReject) : IDomainEvent;

}
