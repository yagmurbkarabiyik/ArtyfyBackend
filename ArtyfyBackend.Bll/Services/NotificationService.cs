using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.Notification;
using ArtyfyBackend.Core.Responses;

namespace ArtyfyBackend.Bll.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IPostService _postService;
	
		public NotificationService(IPostService postService)
		{
			_postService = postService;
		}

		public async Task<Response<List<NotificationModel>>> GetNotificationsByUserId(string userAppId)
		{
			List<NotificationModel> posts = await _postService.GetUserPostsNotifications(userAppId);

			return Response<List<NotificationModel>>.Success(posts, 200);
		}

	}
}