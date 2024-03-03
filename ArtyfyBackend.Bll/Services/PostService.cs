using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Post;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static ArtyfyBackend.Core.Settings.EmailSettings;

namespace ArtyfyBackend.Bll.Services
{
	public class PostService : IPostService
	{
		private readonly IMapper _mapper;
		private readonly UserManager<UserApp> _userManager;
		private readonly IConfiguration _configuration;
		private readonly IPostRepository _postRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PostService(IMapper mapper, UserManager<UserApp> userManager, IConfiguration configuration, IPostRepository postRepository, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
			_postRepository = postRepository;
			_unitOfWork = unitOfWork;
		}

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
	}
}