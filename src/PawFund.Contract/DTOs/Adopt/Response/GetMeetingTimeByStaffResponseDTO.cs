namespace PawFund.Contract.DTOs.Adopt.Response;

public static class GetMeetingTimeByStaffResponseDTO
{
    public class MeetingTimeDTO
    {
        public DateTime MeetingTime { get; set; }
        public int NumberOfStaffsFree { get; set; }
    }
}
