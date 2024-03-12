using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Post;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.IServices
{
	public interface IPostService
	{
		Task<Response<NoDataModel>> Create(PostModel model);
		Task<Response<NoDataModel>> LikePost(int postId, string userId);
        Task<Response<List<PostModel>>> ListSellableProduct();
        Task<Response<List<Post>>> GetAll();	
		//Task<Response<Post>> SavePost();
	}
}