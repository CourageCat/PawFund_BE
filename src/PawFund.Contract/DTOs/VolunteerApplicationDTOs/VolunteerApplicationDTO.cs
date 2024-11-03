using PawFund.Contract.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.DTOs.VolunteerApplicationDTOs
{
    public class VolunteerApplicationDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string ReasonReject { get; set; }
        public Guid EventId { get; set; }
        public Guid EventActivityId { get; set; }
        public AccountDto Account { get; set; }
    }
}
