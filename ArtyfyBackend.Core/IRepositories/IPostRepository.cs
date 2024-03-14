using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        public Task<List<Post>> Like(int postId);
        public Task<List<Post>> GetSellableProductsAsync();
        public Task<List<Post>> SavePost(int postId);
        public Task<List<Post>> LikePost(int postId);
    }
}