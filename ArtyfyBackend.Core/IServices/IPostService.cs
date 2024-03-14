using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Post;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.IServices
{
	public interface IPostService
	{
		Task<Response<NoDataModel>> Create(PostModel model);
		Task<Response<PostModel>> LikePost(int postId, string userId);
        Task<Response<List<PostModel>>> ListSellableProduct();
        Task<Response<List<Post>>> GetAll();
		Task<Response<List<PostModel>>> SavePost(int postId, string userId);
		Task<Response<List<UserSavedPost>>> GetSavedPost(string userId);
		Task<Response<List<UserLikedPost>>> GetLikedPost(string userId);
		Task<Response<List<Post>>> TrendPosts();	
	}
}