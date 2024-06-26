﻿using ArtyfyBackend.Core.IRepositories;
using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.Comment;
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
using Microsoft.Extensions.Configuration;

namespace ArtyfyBackend.Bll.Services
{
    public class PostService : IPostService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly IUserLikedPostRepository _userLikedPostRepository;
        private readonly IUserSavedPostRepository _userSavedPostRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ArtyfyBackendDbContext _context;
        private readonly IMapper _mapper;
        private string? _baseImageUrl;
        public PostService(UserManager<UserApp> userManager, IPostRepository postRepository, IUnitOfWork unitOfWork, ArtyfyBackendDbContext context, IMapper mapper, IUserLikedPostRepository userLikedPostRepository, IUserSavedPostRepository userSavedPostRepository, ICommentRepository commentRepository, IConfiguration configuration)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
            _userLikedPostRepository = userLikedPostRepository;
            _userSavedPostRepository = userSavedPostRepository;
            _commentRepository = commentRepository;
            _baseImageUrl = configuration["BaseImageUrl"];
        }

        /// <summary>
        /// This method lists all posts
        /// </summary>
        public async Task<Response<List<GetPostModel>>> GetAll(string userAppId)
        {
            var postList = await _postRepository
                .Queryable()
                .Include(c => c.Comments)
                .Include(ulp => ulp.UserLikedPosts)
                .Include(up => up.UserPostImages)
                .Select(x => new GetPostModel()
                {
                    PostId = x.Id,
                    Price = x.Price,
                    Title = x.Title,
                    Content = x.Content,
                    LikeCount = x.LikeCount,
                    IsLikeIt = x.UserLikedPosts.Any(ul => ul.UserAppId == userAppId && ul.PostId == x.Id),
                    SaveCount = x.SaveCount,
                    IsSaveIt = x.UserSavedPosts.Any(ul => ul.UserAppId == userAppId && ul.PostId == x.Id),
                    IsSellable = x.IsSellable,
                    UserFullName = x.UserApp.FullName,
                    UserProfileImage = !string.IsNullOrEmpty(x.UserApp.ImageUrl) ? string.Concat(_baseImageUrl, x.UserApp.ImageUrl) : "",
                    UserAppId = x.UserApp.Id,
                    UserName = x.UserApp.UserName,
                    CategoryName = x.Category.Name,
                    Comments = x.Comments.Select(y => new GetCommentModel()
                    {
                        PostId = y.PostId,
                        Title = y.UserApp.FullName,
                        Subtitle = y.Content,
                        Avatar = y.UserApp.ImageUrl
                    }).ToList(),
                    Images = x.UserPostImages.Select(x => string.Concat(_baseImageUrl, x.ImageUrl)).ToList()
                })
                .OrderByDescending(p => p.PostId)
                .Take(20)
                .ToListAsync();

            return Response<List<GetPostModel>>.Success(postList, 200);
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
                    Price = model.Price,
                    Title = model.Title,
                    Content = model.Content,
                    IsSellable = model.IsSellable,
                    UserAppId = model.AppUserId,
                    CategoryId = model.CategoryId,
                    CreatedDate = DateTime.Now,
                    UserPostImages = model.Images.Select(x => new UserPostImage()
                    {
                        ImageUrl = x
                    }).ToList()
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
        public async Task<Response<GetPostModel>> PostDetail(int postId)
        {
            var post = await _postRepository
                .Queryable()
                .Include(x => x.Comments)
                .Include(y => y.UserPostImages)
                .Where(x => x.Id == postId)
                .Select(x => new GetPostModel()
                {
                    PostId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    LikeCount = x.LikeCount,
                    SaveCount = x.SaveCount,
                    IsSellable = x.IsSellable,
                    UserAppId = x.UserApp.Id,
                    UserFullName = x.UserApp.FullName,
                    UserProfileImage = string.Concat(_baseImageUrl, x.UserApp.ImageUrl),
                    CategoryName = x.Category.Name,
                    Comments = x.Comments.Select(y => new GetCommentModel()
                    {
                        PostId = y.PostId,
                        Title = y.UserApp.FullName,
                        Subtitle = y.Content,
                        Avatar = y.UserApp.ImageUrl
                    }).ToList(),
                    Images = x.UserPostImages.Select(x => string.Concat(_baseImageUrl, x.ImageUrl)).ToList()
                }).FirstOrDefaultAsync();

            if (post is null)
            {
                return Response<GetPostModel>.Fail("Post not found", 404, true);
            }

            return Response<GetPostModel>.Success(post, 200);
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

                await _userLikedPostRepository.AddAsync(newUserLikedPost);

                var post = await _postRepository
                    .Queryable()
                    .FirstOrDefaultAsync(p => p.Id == postId);

                if (post != null)
                {
                    post.LikeCount++;
                    _postRepository.Update(post);
                }
            }
            else
            {
                var likedPost = await _userLikedPostRepository
                    .Queryable()
                    .FirstOrDefaultAsync(x => x.UserAppId == userId && x.PostId == postId);

                if (likedPost is null)
                    return Response<PostModel>.Fail("User like not found", 404, true);

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
        public async Task<Response<PostModel>> SavePost(int postId, string userId)
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

                await _userSavedPostRepository.AddAsync(newUserSavedPost);

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

                if (savedPost is null)
                    return Response<PostModel>.Fail("User saved not found", 404, true);

                _userSavedPostRepository.Remove(savedPost);

                var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
                if (post != null && post.SaveCount > 0)
                {
                    post.SaveCount--;
                    _context.Posts.Update(post);
                }
            }
            await _context.SaveChangesAsync();

            return Response<PostModel>.Success("Post saved!", 200);
        }

        /// <summary>
        /// This method listed all sellable products which shared by a user as a post.
        /// </summary>
        public async Task<Response<List<GetPostModel>>> ListSellableProduct(string userAppId)
        {
            var postList = await _postRepository
                .Queryable()
                .Include(c => c.Comments)
                .Include(ulp => ulp.UserLikedPosts)
                .Include(up => up.UserPostImages)
                .Where(p => p.IsSellable == true)
                .Select(x => new GetPostModel()
                {
                    PostId = x.Id,
                    Price = x.Price,
                    Title = x.Title,
                    Content = x.Content,
                    LikeCount = x.LikeCount,
                    IsLikeIt = x.UserLikedPosts.Any(ul => ul.UserAppId == userAppId && ul.PostId == x.Id),
                    SaveCount = x.SaveCount,
                    IsSaveIt = x.UserSavedPosts.Any(ul => ul.UserAppId == userAppId && ul.PostId == x.Id),
                    IsSellable = x.IsSellable,
                    UserFullName = x.UserApp.FullName,
                    UserProfileImage = !string.IsNullOrEmpty(x.UserApp.ImageUrl) ? string.Concat(_baseImageUrl, x.UserApp.ImageUrl) : "",
                    UserAppId = x.UserApp.Id,
                    UserName = x.UserApp.UserName,
                    CategoryName = x.Category.Name,
                    Comments = x.Comments.Select(y => new GetCommentModel()
                    {
                        PostId = y.PostId,
                        Title = y.UserApp.FullName,
                        Subtitle = y.Content,
                        Avatar = y.UserApp.ImageUrl
                    }).ToList(),
                    Images = x.UserPostImages.Select(x => string.Concat(_baseImageUrl, x.ImageUrl)).ToList()
                })
                .OrderByDescending(p => p.PostId)
                .Take(20)
                .ToListAsync();

            return Response<List<GetPostModel>>.Success(postList, 200);
        }

        /// <summary>
        /// This method listed saved posts by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Response<List<GetPostModel>>> GetSavedPost(string userId)
        {
            var savedPostIds = await _context.UserSavedPosts
                .Where(x => x.UserAppId == userId)
                .Select(x => x.PostId)
                .ToListAsync();

            var postList = await _postRepository
                .Queryable()
                .Where(x => savedPostIds.Contains(x.Id))
                .Include(c => c.Comments)
                .Include(ulp => ulp.UserLikedPosts)
                .Include(up => up.UserPostImages)
                .Select(x => new GetPostModel()
                {
                    PostId = x.Id,
                    Price = x.Price,
                    Title = x.Title,
                    Content = x.Content,
                    LikeCount = x.LikeCount,
                    IsLikeIt = x.UserLikedPosts.Any(ul => ul.UserAppId == userId && ul.PostId == x.Id),
                    SaveCount = x.SaveCount,
                    IsSaveIt = true, // Kullanıcının kaydettiği bir post olduğu için true olarak ayarlayabilirsiniz
                    IsSellable = x.IsSellable,
                    UserFullName = x.UserApp.FullName,
                    UserProfileImage = !string.IsNullOrEmpty(x.UserApp.ImageUrl) ? string.Concat(_baseImageUrl, x.UserApp.ImageUrl) : "",
                    UserAppId = x.UserApp.Id,
                    UserName = x.UserApp.UserName,
                    CategoryName = x.Category.Name,
                    Comments = x.Comments.Select(y => new GetCommentModel()
                    {
                        PostId = y.PostId,
                        Title = y.UserApp.FullName,
                        Subtitle = y.Content,
                        Avatar = y.UserApp.ImageUrl
                    }).ToList(),
                    Images = x.UserPostImages.Select(y => string.Concat(_baseImageUrl, y.ImageUrl)).ToList()
                })
                .ToListAsync();

            return Response<List<GetPostModel>>.Success(postList, 200);
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
        public async Task<Response<List<GetPostModel>>> TrendPosts(string userAppId)
        {
            var postList = await _postRepository
                .Queryable()
                .Include(c => c.Comments)
                .Include(ulp => ulp.UserLikedPosts)
                .Include(up => up.UserPostImages)
                .Select(x => new GetPostModel()
                {
                    PostId = x.Id,
                    Price = x.Price,
                    Title = x.Title,
                    Content = x.Content,
                    LikeCount = x.LikeCount,
                    IsLikeIt = x.UserLikedPosts.Any(ul => ul.UserAppId == userAppId && ul.PostId == x.Id),
                    SaveCount = x.SaveCount,
                    IsSaveIt = x.UserSavedPosts.Any(ul => ul.UserAppId == userAppId && ul.PostId == x.Id),
                    IsSellable = x.IsSellable,
                    UserFullName = x.UserApp.FullName,
                    UserProfileImage = !string.IsNullOrEmpty(x.UserApp.ImageUrl)
                        ? string.Concat(_baseImageUrl, x.UserApp.ImageUrl)
                        : "",
                    UserAppId = x.UserApp.Id,
                    UserName = x.UserApp.UserName,
                    CategoryName = x.Category.Name,
                    Comments = x.Comments.Select(y => new GetCommentModel()
                    {
                        PostId = y.PostId,
                        Title = y.UserApp.FullName,
                        Subtitle = y.Content,
                        Avatar = y.UserApp.ImageUrl
                    }).ToList(),
                    Images = x.UserPostImages.Select(x => string.Concat(_baseImageUrl, x.ImageUrl)).ToList()
                }).OrderByDescending(x => x.LikeCount)
                  .ToListAsync();
            
            return Response<List<GetPostModel>>.Success(postList, 200);
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
                    PostId = x.PostId,
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
                    PostId = x.PostId,
                    UserFullName = x.UserApp.FullName,
                    ImageUrl = x.UserApp.ImageUrl,
                    NotificationType = NotificationType.Save
                }).ToListAsync();

            List<NotificationModel> userComments = await _commentRepository
                .Queryable()
                .Where(x => userPostIds.Contains(x.PostId))
                .Select(x => new NotificationModel
                {
                    UserId = x.UserAppId,
                    PostId = x.PostId,
                    UserFullName = x.UserApp.FullName,
                    ImageUrl = x.UserApp.ImageUrl,
                    NotificationType = NotificationType.Comment
                }).ToListAsync();

            var combinedNotifications = userLikedPosts.Concat(userSavedPosts).Concat(userComments).ToList();

            return combinedNotifications;
        }
    }
}