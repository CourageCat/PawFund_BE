using PawFund.Domain.Abstractions.Entities;

namespace PawFund.Domain.Entities;
public class Product : DomainEntity<Guid>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
