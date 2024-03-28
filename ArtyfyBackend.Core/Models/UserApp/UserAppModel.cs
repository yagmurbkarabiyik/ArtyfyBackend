namespace ArtyfyBackend.Core.Models.UserApp
{
    public class UserAppModel
    {
        public string Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
		public string City { get; set; } = null!;
		public string? UserName { get; set; }
        public string PhoneNumber { get; set; } = null!;
		public DateTime? BirthDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}