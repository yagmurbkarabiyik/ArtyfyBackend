namespace ArtyfyBackend.Core.Models.Comment
{
    public class CommentModel
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}