using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Repositories;

public interface IImageCatRepository : IRepositoryBase<ImageCat, Guid>
{
    Task DeleteImageCatAsync(List<Guid> imagesCatId, Guid catId);
}

