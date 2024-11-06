using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.VolunteerApplicationDetail
{
    public static class Query
    {
        public record GetVolunteerApplicationByIdQuery
        (Guid Id) : IQuery<Success<Respone.VolunteerApplicationResponse>>;

        public record GetVolunteerApplicationByActivityQuery
        (Guid Id, int PageIndex,
         int PageSize,
         Filter.VolunteerApplicationFilter FilterParams,
         string[] SelectedColumns) : IQuery<Success<PagedResult<VolunteerApplicationsDTO>>>;
    }
}
