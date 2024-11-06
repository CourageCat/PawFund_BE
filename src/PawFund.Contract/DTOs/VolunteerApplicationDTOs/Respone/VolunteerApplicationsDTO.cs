using PawFund.Contract.Enumarations.VolunteerApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone.GetVolunteerApplicationById;

namespace PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone
{
    public class VolunteerApplicationsDTO
    {
        public Guid Id { get; set; }
        public VolunteerApplicationStatus Status { get; set; }
        public string Description { get; set; }
        public string ReasonReject { get; set; }
        public Guid EventId { get; set; }
        public Guid EventActivityId { get; set; }
        public AccountDTO Account { get; set; }
    }
}
