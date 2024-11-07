
namespace PawFund.Contract.DTOs.Event
{
    public class EventForUserDTO
    {

        public class EventDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Description { get; set; }
            public int MaxAttendees { get; set; }
            public string Status { get; set; }
            public string? ThumbHeroUrl { get; set; }
            public string? ImagesUrl { get; set; }
            public BranchDTO Branch { get; set; }

        }

        public class BranchDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumberOfBranch { get; set; }
            public string EmailOfBranch { get; set; }
            public string Description { get; set; }
            public string NumberHome { get; set; }
            public string StreetName { get; set; }
            public string Ward { get; set; }
            public string District { get; set; }
            public string Province { get; set; }
            public string PostalCode { get; set; }
        }
    }
}
