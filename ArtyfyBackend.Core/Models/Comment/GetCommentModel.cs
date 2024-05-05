namespace ArtyfyBackend.Core.Models.Comment
{
	public class GetCommentModel
	{
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Avatar { get; set; }
    }
}