namespace ArtyfyBackend.Domain.Entities
{
	public class Post : BaseEntity
	{
		public string Title { get; set; } = null!;
		public string Content { get; set; } = null!;
		public string? Image { get; set; }
		public int? LikeCount { get; set; }
		public int? SaveCount { get; set; }
		public bool IsSellable { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public UserApp UserApp { get; set; } = null!;
		public string UserAppId { get; set; } = null!;
        public Category Category { get; set; } = null!;
		public int? CategoryId { get; set; }
		public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<UserLikedPost> UserLikedPosts { get; set; } = new List<UserLikedPost>();	//empty list
        public ICollection<UserSavedPost> UserSavedPosts { get; set; } = new List<UserSavedPost>();	
        public ICollection<UserPostImage> UserPostImages { get; set; } = new List<UserPostImage>();	
    }
}