namespace ArtyfyBackend.Core.Models.SubComment
{
    public class SubCommentModel
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; } = null!;
    }
}