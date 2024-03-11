using ArtyfyBackend.Core.Models.SubComment;

namespace ArtyfyBackend.Core.Models.Comment
{
    public class CommentWithSubCommentModel
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<SubCommentModel> SubComments { get; set; }
    }
}