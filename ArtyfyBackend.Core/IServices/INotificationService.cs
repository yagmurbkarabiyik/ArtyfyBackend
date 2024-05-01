using ArtyfyBackend.Core.Models.Notification;
using ArtyfyBackend.Core.Responses;

namespace ArtyfyBackend.Core.IServices
{
	public interface INotificationService
	{
		Task<Response<List<NotificationModel>>> GetNotificationsByUserId(string userAppId);
	}
}