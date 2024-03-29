﻿using ArtyfyBackend.Core.IServices;
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


        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _postService.GetAll();
            return CreateActionResult(response);
        }

        [HttpPost("create")]
		public async Task<IActionResult> CreatePost([FromBody] PostModel model)
		{
			return CreateActionResult(await _postService.Create(model));
		}

		[HttpGet("like")]
		public async Task<IActionResult> Like(int postId, string userId)
		{
			var response = await _postService.LikePost(postId, userId);

			return CreateActionResult(response);
		}

        /// <summary>
        /// This method listed sellable products to show our market.
        /// </summary>
        [HttpGet("getSellableProducts")]
        public async Task<IActionResult> GetSellableProducts()
        {
            return CreateActionResult(await _postService.ListSellableProduct());
        }

		[HttpGet("save")]
		public async Task<IActionResult> Save(int postId, string userId)
		{
			var response = await _postService.SavePost(postId, userId);

			return CreateActionResult(response);
		}

		[HttpGet("listSavedPosts")]
		public async Task<IActionResult> ListSavedPost( string userId)
		{
			var response = await _postService.GetSavedPost(userId);

			return CreateActionResult(response);
		}

		[HttpGet("listLikedPosts")]
		public async Task<IActionResult> ListLikedPost(string userId)
		{
			var response = await _postService.GetLikedPost(userId);

            return CreateActionResult(response);
        }

        [HttpGet("trendPosts")]
		public async Task<IActionResult> TrendPosts()
		{
			var response = await _postService.TrendPosts();	
			return CreateActionResult(response);
		}
    }
}
