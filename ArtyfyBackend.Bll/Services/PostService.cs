using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Post;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;
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
        public PostService(UserManager<UserApp> userManager, IPostRepository postRepository, IUnitOfWork unitOfWork, ArtyfyBackendDbContext context)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _context = context;
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
                    UserAppId = model.AppUserId,
                    Image = model.Image ?? "",
                    Content = model.Content,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
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
        public async Task<Response<NoDataModel>> LikePost(int postId, string userId)
        {
            try
            {
                // Kullanıcıyı bul
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    return Response<NoDataModel>.Fail("User not found!", 404, false);
                }

                var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
                if (post == null)
                {
                    return Response<NoDataModel>.Fail("Post not found!", 404, false);
                }

                if (post.LikeCount > 0)
                {
                    return Response<NoDataModel>.Fail("You already liked this post!", 400, false);
                }

                post.LikeCount++;

                _context.Posts.Update(post);

                await _context.SaveChangesAsync();

                return Response<NoDataModel>.Success("Post liked!", 200);
            }
            catch (Exception ex)
            {
                return Response<NoDataModel>.Fail("Something went wrong: " + ex.Message, 400, false);
            }
        }
    }
}