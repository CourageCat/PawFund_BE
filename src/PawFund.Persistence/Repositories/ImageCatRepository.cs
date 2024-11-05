using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class ImageCatRepository(ApplicationDbContext context) : RepositoryBase<ImageCat, Guid>(context), IImageCatRepository
{
}
