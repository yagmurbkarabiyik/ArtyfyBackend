using ArtyfyBackend.Core.IRepositories;
using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Notification;
using ArtyfyBackend.Core.Models.Post;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;
using ArtyfyBackend.Domain.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArtyfyBackend.Bll.Services
{
	public class PostService : IPostService
	{
		private readonly UserManager<UserApp> _userManager;
		private readonly IPostRepository _postRepository;
		private readonly IUserLikedPostRepository _userLikedPostRepository;
		private readonly IUserSavedPostRepository _userSavedPostRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ArtyfyBackendDbContext _context;
		private readonly IMapper _mapper;
		public PostService(UserManager<UserApp> userManager, IPostRepository postRepository, IUnitOfWork unitOfWork, ArtyfyBackendDbContext context, IMapper mapper, IUserLikedPostRepository userLikedPostRepository, IUserSavedPostRepository userSavedPostRepository)
		{
			_userManager = userManager;
			_postRepository = postRepository;
			_unitOfWork = unitOfWork;
			_context = context;
			_mapper = mapper;
			_userLikedPostRepository = userLikedPostRepository;
			_userSavedPostRepository = userSavedPostRepository;
		}

		/// <summary>
		/// This method lists all posts
		/// </summary>
		public async Task<Response<List<Post>>> GetAll()
		{
			var posts = await _context.Posts.ToListAsync();

			return Response<List<Post>>.Success(posts, 200);
		}

		/// <summary>
		/// This method used for create a post
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<Response<NoDataModel>> Create(PostCreateModel model)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(model.AppUserId);

				if (user == null)
				{
					return Response<NoDataModel>.Fail("Kullanıcı bulunamadı!", 404, false);
				}

				var post = new Post
				{
					Title = model.Title,
					Content = model.Content,
					Image = model.Image ?? "",
					IsSellable = model.IsSellable,
					UserAppId = model.AppUserId,
					CategoryId = model.CategoryId,
					CreatedDate = DateTime.Now
				};

				await _postRepository.AddAsync(post);

				await _unitOfWork.CommitAsync();

				return Response<NoDataModel>.Success("Yeni gönderi oluşturuldu!", 200);
			}
			catch (Exception ex)
			{
				return Response<NoDataModel>.Fail($"Gönderi oluşturulurken bir hata oluştu: {ex.Message}", 500, false);
			}
		}

		/// <summary>
		/// This method used for get post's detail
		/// </summary>
		/// <param name="postId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<Response<Post>> PostDetail(int postId)
		{
			var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);

			return Response<Post>.Success(post, 200);
		}

		/// <summary>
		/// Thist method used for like a post. A user can like a post only once
		/// </summary>
		/// <param name="postId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<Response<PostModel>> LikePost(int postId, string userId)
		{
			var isPostLiked = await _userLikedPostRepository
				.Queryable()
				.AnyAsync(x => x.UserAppId == userId && x.PostId == postId);

			if (!isPostLiked)
			{
				var newUserLikedPost = new UserLikedPost
				{
					UserAppId = userId,
					PostId = postId
				};

				_userLikedPostRepository.AddAsync(newUserLikedPost);

				var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
				if (post != null)
				{
					post.LikeCount++;
					_context.Posts.Update(post);
				}
			}
			else
			{
				var likedPost = await _userLikedPostRepository
					.Queryable()
					.FirstOrDefaultAsync(x => x.UserAppId == userId && x.PostId == postId);

				_userLikedPostRepository.Remove(likedPost);

				var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
				if (post != null && post.LikeCount > 0)
				{
					post.LikeCount--;
					_context.Posts.Update(post);
				}
			}

			await _context.SaveChangesAsync();

			return Response<PostModel>.Success(200);
		}

		/// <summary>
		/// This method used for save post
		/// </summary>
		/// <param name="postId"></param>
		/// <param name="userId"></param>
		public async Task<Response<List<PostModel>>> SavePost(int postId, string userId)
		{
			var isPostSaved = await _userSavedPostRepository
				.Queryable()
				.AnyAsync(x => x.UserAppId == userId && x.PostId == postId);

			if (!isPostSaved)
			{
				var newUserSavedPost = new UserSavedPost
				{
					UserAppId = userId,
					PostId = postId
				};

				_userSavedPostRepository.AddAsync(newUserSavedPost);

				var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
				if (post != null)
				{
					post.SaveCount++;
					_context.Posts.Update(post);
				}
			}
			else
			{
				var savedPost = await _userSavedPostRepository
					.Queryable()
					.FirstOrDefaultAsync(x => x.UserAppId == userId && x.PostId == postId);

				_userSavedPostRepository.Remove(savedPost);

				var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
				if (post != null && post.SaveCount > 0)
				{
					post.SaveCount--;
					_context.Posts.Update(post);
				}
			}
			await _context.SaveChangesAsync();

			return Response<List<PostModel>>.Success("Post saved!", 200);

			//var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
			//if (user == null)
			//	return Response<List<PostModel>>.Fail("User not found!", 404, false);

			//var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
			//if (post == null)
			//	return Response<List<PostModel>>.Fail("Post not found!", 404, false);

			//var existingUserSavedPost = await _context.UserSavedPosts
			//	.FirstOrDefaultAsync(up => up.UserAppId == userId && up.PostId == postId);

			//if (existingUserSavedPost != null)
			//	return Response<List<PostModel>>.Fail("Post already saved by the user!", 400, false);

			//var newUserSavedPost = new UserSavedPost
			//{
			//	UserAppId = userId,
			//	PostId = postId
			//};

			//_context.UserSavedPosts.Add(newUserSavedPost);

			//post.SaveCount++;

			//_context.Posts.Update(post);

			//await _context.SaveChangesAsync();

			//return Response<List<PostModel>>.Success("Post saved!", 200);
		}

		/// <summary>
		/// This method listed all sellable products which shared by a user as a post.
		/// </summary>
		public async Task<Response<List<PostModel>>> ListSellableProduct()
		{
			var sellableProducts = await _postRepository.GetSellableProductsAsync();

			var products = _mapper.Map<List<PostModel>>(sellableProducts);

			return Response<List<PostModel>>.Success(products, 200);
		}

		/// <summary>
		/// This method listed saved posts by user
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<Response<List<UserSavedPost>>> GetSavedPost(string userId)
		{
			var posts = await _context.UserSavedPosts.Where(x => x.UserAppId == userId).ToListAsync();

			return Response<List<UserSavedPost>>.Success(posts, 200);
		}

		/// <summary>
		/// This method listed liked posts by user
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<Response<List<UserLikedPost>>> GetLikedPost(string userId)
		{
			var posts = await _context.UserLikedPosts.Where(x => x.UserAppId == userId).ToListAsync();

			return Response<List<UserLikedPost>>.Success(posts, 200);
		}

		/// <summary>
		/// This method used for trend page. It shows us post which have the most like count
		/// </summary>
		/// <returns></returns>
		public async Task<Response<List<Post>>> TrendPosts()
		{
			var trendingPosts = await _context.Posts
			   .OrderByDescending(p => p.LikeCount)
			   .ToListAsync();

			return Response<List<Post>>.Success(trendingPosts, 200);
		}

		/// <summary>
		/// This method returns us a detailed notification of the interaction between the post and the users. For example, this post was liked by user x, commented on by user x, or saved by user x.
		/// </summary>
		/// <param name="userAppId"></param>
		/// <returns></returns>
		public async Task<List<NotificationModel>> GetUserPostsNotifications(string userAppId)
		{
			List<int> userPostIds = await _postRepository
				.Queryable()
				.Where(x => x.UserAppId == userAppId)
				.Select(x => x.Id)
				.ToListAsync();

			List<NotificationModel> userLikedPosts = await _userLikedPostRepository
				.Queryable()
				.Where(x => userPostIds.Contains(x.PostId))
				.Select(x => new NotificationModel
				{
					UserId = x.UserAppId,
					UserFullName = x.UserApp.FullName,
					ImageUrl = x.UserApp.ImageUrl,
					NotificationType = NotificationType.Like
				}).ToListAsync();

			List<NotificationModel> userSavedPosts = await _userSavedPostRepository
				.Queryable()
				.Where(x => userPostIds.Contains(x.PostId))
				.Select(x => new NotificationModel
				{
					UserId = x.UserAppId,
					UserFullName = x.UserApp.FullName,
					ImageUrl = x.UserApp.ImageUrl,
					NotificationType = NotificationType.Save
				}).ToListAsync();


			var combinedNotifications = userLikedPosts.Concat(userSavedPosts).ToList();

			return combinedNotifications;
		}
	}
}
//todo save/like already saved/liked check problem