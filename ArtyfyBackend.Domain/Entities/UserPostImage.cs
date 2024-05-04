namespace ArtyfyBackend.Domain.Entities
{
    public class UserPostImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string UserAppId { get; set; }
        public UserApp UserApp { get; set; }
    }
}