using PawFund.Contract.Abstractions.Message;

namespace PawFund.Contract.Services.AdoptApplications;
public static class DomainEvent
{
    public record AdoptionHasBeenApproved(Guid Id, string Email, string CatName) : IDomainEvent;
    public record AdoptionHasBeenRejected(Guid Id, string Email, string CatName, string ReasonReject) : IDomainEvent;
    public record AdoptionHasBeenCompleted(Guid Id, string Email, string CatName) : IDomainEvent;
    public record AdoptionHasBeenRejectedOutside(Guid Id, string Email, string CatName, string ReasonReject) : IDomainEvent;

}
