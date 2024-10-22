namespace PawFund.Contract.DTOs.Adopt.Response;

public static class GetMeetingTimeByAdopterResponseDTO
{
    public class MeetingTimeDTO
    {
        public DateTime MeetingTime { get; set; }
        public int NumberOfStaffsFree { get; set; }
    }
}
