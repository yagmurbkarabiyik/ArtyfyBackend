namespace ArtyfyBackend.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = null!;

        /// <summary>
        /// Relations
        /// </summary>
        public string UserAppId { get; set; } = null!;
        public UserApp? UserApp { get; set; }
        public Post Post { get; set; } = null!;
        public int PostId { get; set; }
    }
}