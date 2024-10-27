namespace PawFund.Contract.Enumarations.AdoptPetApplication;

public enum AdoptPetApplicationStatus
{
    Pending = 0,
    Rejected = -1,
    Approved = 1,
    ApprovedAndCompleted = 2,
    ApprovedAndNotCompleted = 3,
}
