namespace ArtyfyBackend.Core.Models.UserApp
{
    public class UserAppModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}