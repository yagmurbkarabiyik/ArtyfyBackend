namespace ArtyfyBackend.Domain.Entities
{
    public class SubComment : BaseEntity
    {
        public string UserId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }

        /// <summary>
        /// Relation
        /// </summary>
        public Comment Comment { get; set; }
    }
}
