using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext context) : RepositoryBase<Product, Guid>(context), IProductRepository
{
}