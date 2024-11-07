using Microsoft.EntityFrameworkCore;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class ImageCatRepository(ApplicationDbContext context) : RepositoryBase<ImageCat, Guid>(context), IImageCatRepository
{
    private readonly ApplicationDbContext _appDbContext = context;

    public async Task DeleteImageCatAsync(List<Guid> imagesCatId, Guid catId)
    {
        var images = await _appDbContext.ImageCats
            .Where(image => !imagesCatId.Contains(image.Id) && image.CatId == catId)
            .ToListAsync();

        _appDbContext.ImageCats.RemoveRange(images);
    }
}
