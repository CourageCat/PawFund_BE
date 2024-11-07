using PawFund.Contract.DTOs.BranchDTOs;

namespace PawFund.Contract.DTOs.EventDTOs.Respone;

public class EventForAdminStaffDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ReasonReject { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? Status { get; set; } = string.Empty;
    public int MaxAttendees { get; set; } = 1;
    public string? ImagesUrl { get; set; }

    public BranchEventDTO Branch { get; set; }
}
