using ArtyfyBackend.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ArtyfyBackend.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : BaseController
	{
		private INotificationService _notificationService;

		public NotificationsController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpGet]
		public async Task<IActionResult> GetUserNotification(string userAppId)
		{
			return CreateActionResult(await _notificationService.GetNotificationsByUserId(userAppId));
		}
	}
}
