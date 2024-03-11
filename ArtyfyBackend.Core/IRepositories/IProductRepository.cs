using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetSellableProductsAsync();
    }
}