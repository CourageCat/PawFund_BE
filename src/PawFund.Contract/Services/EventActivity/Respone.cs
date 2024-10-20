using static PawFund.Contract.DTOs.EventActivity.GetEventActivityByIdDTO;

namespace PawFund.Contract.Services.EventActivity
{
    public static class Respone
    {
        public record EventActivityResponse(DTOs.EventActivity.GetEventActivityByIdDTO.ActivityDTO ActivityDTO, DTOs.EventActivity.GetEventActivityByIdDTO.EventDTO EventDTO);

    }
}
