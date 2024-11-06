namespace PawFund.Contract.DTOs.VolunteerApplicationDTOs.Request
{
    public class CreateVolunteerApplicationDTO
    {
        public Guid eventId {  get; set; }
        public List<string> listActivity {  get; set; }
        public string description {  get; set; }
    }
}
