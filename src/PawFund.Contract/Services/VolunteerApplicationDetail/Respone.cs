using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone;

namespace PawFund.Contract.Services.VolunteerApplicationDetail
{
    public static class Respone
    {
        public record VolunteerApplicationResponse(GetVolunteerApplicationById.VolunteerApplicationDTO VolunteerApplicationDTO, GetVolunteerApplicationById.AccountDTO AccountDTO);
    }
}
