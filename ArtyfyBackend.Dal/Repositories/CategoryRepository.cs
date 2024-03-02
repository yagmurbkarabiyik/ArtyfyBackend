using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Dal.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ArtyfyBackendDbContext context) : base(context)
        {
        }
    }
}