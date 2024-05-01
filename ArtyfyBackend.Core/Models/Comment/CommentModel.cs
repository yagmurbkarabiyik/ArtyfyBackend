namespace ArtyfyBackend.Core.Models.Comment
{
    public class CommentModel
    {
        public int? Id { get; set; }
        public string UserAppId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}