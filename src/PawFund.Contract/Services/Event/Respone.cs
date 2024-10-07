
namespace PawFund.Contract.Services.Event
{
    public static class Respone
    {
        public record EventResponse(DTOs.Event.GetEventByIdDTO.EventDTO EventDTO, DTOs.Event.GetEventByIdDTO.BranchDTO BranchDTO);
    }
}
