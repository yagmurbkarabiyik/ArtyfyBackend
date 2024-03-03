namespace ArtyfyBackend.Core.Models.Post
{
	public class PostCreateModel
	{
		public string AppUserId { get; set; } = null!;
		public string? Image { get; set; }
		public string Content { get; set; } = null!;
		public int? LikeCount { get; set; }
	}
}
