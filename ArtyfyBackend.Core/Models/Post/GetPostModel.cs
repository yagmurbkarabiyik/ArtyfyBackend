using ArtyfyBackend.Core.Models.Comment;

namespace ArtyfyBackend.Core.Models.Post
{
	public class GetPostModel
	{
		public string Title { get; set; } = null!;
		public string Content { get; set; } = null!;
		public string? Image { get; set; }
		public int? LikeCount { get; set; }
        public bool IsLikeIt { get; set; }
        public int? SaveCount { get; set; }
        public bool IsSaveIt { get; set; }
        public bool IsSellable { get; set; }
        public string UserFullName { get; set; }
        public string CategoryName { get; set; }
		public List<GetCommentModel> Comments { get; set; }
	}
}