using PawFund.Domain.Entities;

namespace PawFund.Domain.Abstractions.Repositories;

public interface IProductRepository : IRepositoryBase<Product, Guid>
{
}