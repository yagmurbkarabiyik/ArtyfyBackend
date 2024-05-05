namespace ArtyfyBackend.Core.Models.Register
{
	public class RegisterModel
	{
		public string FullName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string? UserName { get; set; }
        public string? UserProfileImage { get; set; }
    }
}