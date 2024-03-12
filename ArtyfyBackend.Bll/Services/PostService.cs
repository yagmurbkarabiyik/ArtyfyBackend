using ArtyfyBackend.Core.IServices;
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

            return Response<List<Post>>.Success(posts, 200); ;
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
        public async Task<Response<NoDataModel>> LikePost(int postId, string userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                    return Response<NoDataModel>.Fail("User not found!", 404, false);
                
                var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
                if (post == null)
                    return Response<NoDataModel>.Fail("Post not found!", 404, false);

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
        /// This method used for save pos
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        public async Task<Response<List<PostModel>>> SavePost(int postId, string userId)
        {
           var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

            //var isPostSavedByUser = await _context.Posts
            //    .AnyAsync(up => up.UserAppId == userId && up.Id == postId);

            //if (isPostSavedByUser)
            //{
            //    return Response<List<PostModel>>.Fail("Post already saved by the user!", 400, false);
            //}

            post.SaveCount++;

            _context.Posts.Update(post);

            await _context.SaveChangesAsync();

            return Response<List<PostModel>>.Success("Post saved!", 200);
        }
    }
}