
using PawFund.Contract.DTOs.BranchDTOs;

namespace PawFund.Contract.DTOs.Event;

public class EventDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; } = string.Empty;
    public BranchEventDTO Branch { get; set; }
}
