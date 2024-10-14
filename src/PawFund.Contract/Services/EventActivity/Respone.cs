using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.EventActivity
{
    public static class Respone
    {
        public record EventActivityResponse(DTOs.EventActivity.GetEventActivityByIdDTO.ActivityDTO ActivityDTO, DTOs.EventActivity.GetEventActivityByIdDTO.EventDTO EventDTO);
    }
}
