using ArtyfyBackend.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string UserAppId { get; set; } = null!;
        public int? CategoryId { get; set; }
    }
}