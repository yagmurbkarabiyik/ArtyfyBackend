using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtyfyBackend.Dal.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ArtyfyBackendDbContext _context;

        public PostRepository(ArtyfyBackendDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Post>> Like(int id)
        {
            return await _context.Posts.Where(x => x.Id == id).ToListAsync();
        }
    }
}
