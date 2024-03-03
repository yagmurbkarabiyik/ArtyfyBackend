using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Post;
using ArtyfyBackend.Core.Responses;

namespace ArtyfyBackend.Core.IServices
{
	public interface IPostService
	{
		Task<Response<NoDataModel>> Create(PostCreateModel model);
	}
}