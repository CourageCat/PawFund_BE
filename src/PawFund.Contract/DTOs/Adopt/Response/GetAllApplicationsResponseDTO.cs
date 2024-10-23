namespace PawFund.Contract.DTOs.Adopt.Response;

public static class GetAllApplicationsResponseDTO
{
    public class AdoptApplicationDTO
    {
        public Guid Id { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string? ReasonReject { get; set; }
        public string Status { get; set; }
        public bool IsFinalized { get; set; }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public AccountDto Account { get; set; }
        public CatDto Cat { get; set; }
    }
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class CatDto
    {
        public Guid Id { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public decimal Weight { get; set; } = 0;
        public string Color { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

