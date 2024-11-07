using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Enumarations.VolunteerApplication;
using PawFund.Domain.Entities;
using static PawFund.Contract.Services.VolunteerApplicationDetail.Filter;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IVolunteerApplicationDetail : IGenericRepository<VolunteerApplicationDetail>
{
    public Task<bool> CheckVolunteerApplicationExists(Guid eventId, Guid accountId);
    Task<int> CountAllVolunteerApplications();

    Task<PagedResult<VolunteerApplicationDetail>> GetAllVolunteerAppicationByActivityIdAsync(Guid id, int pageIndex, int pageSize, VolunteerApplicationFilter filterParams, string[] selectedColumns);

}

