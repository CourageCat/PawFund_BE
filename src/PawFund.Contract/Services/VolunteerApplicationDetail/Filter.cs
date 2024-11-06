using PawFund.Contract.Enumarations.Event;
using PawFund.Contract.Enumarations.VolunteerApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.VolunteerApplicationDetail
{
    public class Filter
    {
        public record VolunteerApplicationFilter(string? Name, VolunteerApplicationStatus? Status, bool IsAscCreatedDate);
    }
}
