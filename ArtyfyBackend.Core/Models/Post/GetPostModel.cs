﻿using ArtyfyBackend.Core.Models.Comment;

namespace ArtyfyBackend.Core.Models.Post
{
	public class GetPostModel
	{
        public int PostId { get; set; }
        public decimal? Price { get; set; }
        public string Title { get; set; } = null!;
		public string Content { get; set; } = null!;
		public List<string> Images { get; set; } = new List<string>();
        public int? LikeCount { get; set; }
        public bool IsLikeIt { get; set; }
        public int? SaveCount { get; set; }
        public bool IsSaveIt { get; set; }
        public bool IsSellable { get; set; }
        public string UserAppId { get; set; }
        public string UserFullName { get; set; }
        public string UserProfileImage { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
		public List<GetCommentModel> Comments { get; set; }
	}
}