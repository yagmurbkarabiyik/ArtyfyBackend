using System.ComponentModel.DataAnnotations;

namespace ArtyfyBackend.Domain.Enums
{
	public enum NotificationType
	{
		[Display(Name = "Like")]
		Like = 1,

		[Display(Name = "Comment")]
		Comment,

		[Display(Name = "Save")]
		Save
	}
}