using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtyfyBackend.Dal.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ArtyfyBackendDbContext _context;

        public ProductRepository(ArtyfyBackendDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetSellableProductsAsync()
        {
            return await _context.Products.Where(p => p.IsSellable == true).ToListAsync();
        }
    }
}