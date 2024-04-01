namespace ArtyfyBackend.Core.Models.Post
{
	public class PostModel
	{
		public string Title { get; set; } = null!;
		public string Content { get; set; } = null!;
		public string? Image { get; set; }
		public int? LikeCount { get; set; }
		public int? SaveCount { get; set; }
		public bool IsSellable { get; set; }
		public string AppUserId { get; set; } = null!;
		public int? CategoryId { get; set; }
		public int PostId { get; set; } 
	}
}