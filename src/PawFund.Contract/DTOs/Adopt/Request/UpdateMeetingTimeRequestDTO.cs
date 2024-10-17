namespace PawFund.Contract.DTOs.Adopt.Request;
public static class UpdateMeetingTimeRequestDTO
{
    public class MeetingTimeDTO
    {
        public DateTime MeetingTime { get; set; }
        public int NumberOfStaffsFree { get; set; }
    }
}
