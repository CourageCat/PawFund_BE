using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Repositories;

public interface IVolunteerApplicationDetail : IRepositoryBase<VolunteerApplicationDetail, Guid>
{
    Task<List<VolunteerApplicationDetail>> FindAllAsync(Guid activityId);
}

