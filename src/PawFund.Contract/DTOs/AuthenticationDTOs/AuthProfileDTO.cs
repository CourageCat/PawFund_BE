namespace PawFund.Contract.DTOs.AuthenticationDTOs;

public sealed class AuthProfileDTO
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
