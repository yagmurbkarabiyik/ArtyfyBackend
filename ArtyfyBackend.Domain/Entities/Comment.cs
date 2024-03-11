namespace ArtyfyBackend.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public string UserId { get; set; }
        public UserApp? UserApp { get; set; }
        public ICollection<SubComment> SubComments { get; set; }
    }
}