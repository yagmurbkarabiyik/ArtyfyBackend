namespace ArtyfyBackend.Core.Models.UserApp
{
    public class UserAppUpdateModel
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? UserName { get; set; }
        public string City { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime? BirthDate { get; set; }
    }
	
}