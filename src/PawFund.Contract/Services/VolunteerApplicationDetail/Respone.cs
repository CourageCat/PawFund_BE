using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.VolunteerApplicationDetail
{
    public static class Respone
    {
        public record VolunteerApplicationResponse(DTOs.VolunteerApplicationDTOs.GetVolunteerApplicationById.VolunteerApplicationDTO VolunteerApplicationDTO, DTOs.VolunteerApplicationDTOs.GetVolunteerApplicationById.AccountDTO AccountDTO);
    }
}
