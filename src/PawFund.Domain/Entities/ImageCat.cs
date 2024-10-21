using PawFund.Domain.Abstractions.Entities;

namespace PawFund.Domain.Entities;

public class ImageCat : DomainEntity<Guid>
{
    public string ImageUrl { get; set; }
    public string PublicImageId { get; set; }
    public Guid CatId { get; set; }
    public virtual Cat Cat { get; set; }
}
