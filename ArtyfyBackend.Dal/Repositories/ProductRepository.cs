using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Dal.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ArtyfyBackendDbContext context) : base(context)
        {
        }
    }
}
