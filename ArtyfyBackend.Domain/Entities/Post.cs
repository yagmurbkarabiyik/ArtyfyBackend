namespace ArtyfyBackend.Domain.Entities
{
	public class Post : BaseEntity
	{
		public string Image { get; set; }
		public string Content { get; set; }
		public int LikeCount { get; set; }

		/// <summary>
		/// Relations
		/// </summary>
		public string UserAppId { get; set; }
		public  UserApp UserApp { get; set; }
	}
}