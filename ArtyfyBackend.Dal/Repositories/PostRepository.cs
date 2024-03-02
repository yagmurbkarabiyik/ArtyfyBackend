using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Dal.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ArtyfyBackendDbContext context) : base(context)
        {
        }
    }
}
