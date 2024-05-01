﻿namespace ArtyfyBackend.Core.Models.Post
{
	public class PostCreateModel
	{
		public string Title { get; set; } = null!;
		public string Content { get; set; } = null!;
		public string? Image { get; set; }
		public bool IsSellable { get; set; }
		public string AppUserId { get; set; } = null!;
		public int? CategoryId { get; set; }
	}
}