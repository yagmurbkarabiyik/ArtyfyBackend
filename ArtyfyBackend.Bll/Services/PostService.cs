﻿using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Post;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Dal.Repositories;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArtyfyBackend.Bll.Services
{
    public class PostService : IPostService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ArtyfyBackendDbContext _context;
        private readonly IMapper _mapper;
        public PostService(UserManager<UserApp> userManager, IPostRepository postRepository, IUnitOfWork unitOfWork, ArtyfyBackendDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
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
        public async Task<Response<NoDataModel>> Create(PostModel model)
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
                    LikeCount = model.LikeCount ?? 0,
                    SaveCount = model.SaveCount ?? 0,
                    UserAppId = model.AppUserId,
                    IsSellable = model.IsSellable,
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
        /// Thist method used for like a post. A user can like a post only once
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Response<PostModel>> LikePost(int postId, string userId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
                return Response<PostModel>.Fail("Post not found!", 404, false);

            var existingUserLikedPost = await _context.UserSavedPosts
                .FirstOrDefaultAsync(up => up.UserAppId == userId && up.PostId == postId);

            if (existingUserLikedPost != null)
                return Response<PostModel>.Fail("Post already liked by the user!", 400, false);

            var newUserLikedPost = new UserLikedPost
            {
                UserAppId = userId,
                PostId = postId
            };

            _context.UserLikedPosts.Add(newUserLikedPost);

            post.LikeCount++;

            _context.Posts.Update(post);

            await _context.SaveChangesAsync();

            return Response<PostModel>.Success("Post liked!", 200);
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
        /// This method used for save post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        public async Task<Response<List<PostModel>>> SavePost(int postId, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return Response<List<PostModel>>.Fail("User not found!", 404, false);

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
                return Response<List<PostModel>>.Fail("Post not found!", 404, false);

            var existingUserSavedPost = await _context.UserSavedPosts
                .FirstOrDefaultAsync(up => up.UserAppId == userId && up.PostId == postId);

            if (existingUserSavedPost != null)
                return Response<List<PostModel>>.Fail("Post already saved by the user!", 400, false);

            var newUserSavedPost = new UserSavedPost
            {
                UserAppId = userId,
                PostId = postId
            };

            _context.UserSavedPosts.Add(newUserSavedPost);

            post.SaveCount++;

            _context.Posts.Update(post);

            await _context.SaveChangesAsync();

            return Response<List<PostModel>>.Success("Post saved!", 200);
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
    }
}
//todo save/like already saved/liked check problem