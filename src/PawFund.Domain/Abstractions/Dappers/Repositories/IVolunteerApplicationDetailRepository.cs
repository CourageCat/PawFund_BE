using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

    public interface IVolunteerApplicationDetailRepository : IGenericRepository<VolunteerApplicationDetail>
    {
        public Task<bool> CheckVolunteerApplicationExists(Guid eventId, Guid accountId);
    }

