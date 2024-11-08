using Microsoft.EntityFrameworkCore;
using PawFund.Domain.Abstractions.Repositories;

namespace PawFund.Persistence.Repositories;

public class VolunteerApplicationDetail(ApplicationDbContext context) : RepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid>(context), IVolunteerApplicationDetail
{
    private readonly ApplicationDbContext _context =context; 
    public async Task<List<Domain.Entities.VolunteerApplicationDetail>> FindAllAsync(Guid activityId)
    {
        return await _context.VolunteerApplicationDetails
        .Where(vad => vad.EventActivityId == activityId && vad.Status == 0)
        .ToListAsync();
    }
}

