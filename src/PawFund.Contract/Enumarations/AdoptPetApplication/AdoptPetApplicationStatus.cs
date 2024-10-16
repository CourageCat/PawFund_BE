using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Enumarations.AdoptPetApplication
{
    public enum AdoptPetApplicationStatus
    {
        Pending = 0,
        Rejected = -1,
        Approved = 1,
        ApprovedAndCompleted = 2,
        ApprovedAndNotCompleted = 3,

    }
}
