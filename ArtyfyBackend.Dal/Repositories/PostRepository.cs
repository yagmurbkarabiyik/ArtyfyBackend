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

        /// <summary>
        /// This method used for like posts.
        /// </summary>
        /// <param name="postId"></param>
        public async Task<List<Post>> Like(int postId)
        {
            return await _context.Posts.Where(x => x.Id == postId).ToListAsync();
        }

        /// <summary>
        /// This method shows us to posts which are able to sell.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Post>> GetSellableProductsAsync()
        {
            return await _context.Posts.Where(p => p.IsSellable == true).ToListAsync();
        }

        /// <summary>
        /// This method used for save posts.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<List<Post>> SavePost(int postId)
        {
            return await _context.Posts.Where(x => x.Id == postId).ToListAsync();
        }

        /// <summary>
        /// This method used for like posts.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<List<Post>> LikePost(int postId)
        {
            return await _context.Posts.Where(x => x.Id == postId).ToListAsync();
        }
    }
}