using PawFund.Domain.Abstractions.Repositories;

namespace PawFund.Persistence.Repositories;

public class VolunteerApplicationDetail(ApplicationDbContext context) : RepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid>(context), IVolunteerApplicationDetail
{
}

