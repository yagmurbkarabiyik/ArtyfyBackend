using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.Post;
using Microsoft.AspNetCore.Mvc;

namespace ArtyfyBackend.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : BaseController
	{
		private readonly IPostService _postService;

		public PostsController(IPostService postService)
		{
			_postService = postService;
		}

		[HttpPost]
		public async Task<IActionResult> CreatePost([FromBody] PostCreateModel model)
		{
			return CreateActionResult(await _postService.Create(model));
		}
	}
}
