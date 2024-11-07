
namespace PawFund.Contract.Services.Event
{
    public static class Respone
    {
        public record EventResponse(DTOs.Event.EventForUserDTO.EventDTO EventDTO);

        public record EventDetailResopnse(string Name, DateTime StartDate, DateTime EndDate, string Description, int MaxAttendees, string Status);
    }
}
