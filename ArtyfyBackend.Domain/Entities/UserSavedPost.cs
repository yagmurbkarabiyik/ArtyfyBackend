namespace ArtyfyBackend.Domain.Entities
{
    public class UserSavedPost : BaseEntity
    {
        public string UserAppId { get; set; } = null!;
        public UserApp? UserApp { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}