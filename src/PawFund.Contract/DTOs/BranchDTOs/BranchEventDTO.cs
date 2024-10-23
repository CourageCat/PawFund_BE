namespace PawFund.Contract.DTOs.BranchDTOs;

public class BranchEventDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumberOfBranch { get; set; }
    public string EmailOfBranch { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string NumberHome { get; set; } = string.Empty;
    public string StreetName { get; set; } = string.Empty;
    public string Ward { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
}
