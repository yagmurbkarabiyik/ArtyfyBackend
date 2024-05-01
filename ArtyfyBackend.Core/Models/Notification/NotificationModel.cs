using ArtyfyBackend.Domain.Enums;

namespace ArtyfyBackend.Core.Models.Notification
{
	public class NotificationModel
	{
		public string UserId { get; set; } = null!;
		public string? UserFullName { get; set; }
        public string? ImageUrl { get; set; } 
		public NotificationType NotificationType { get; set; } 
	}
}