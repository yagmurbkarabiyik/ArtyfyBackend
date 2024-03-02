namespace ArtyfyBackend.Core.Models.UserApp
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
    }
}
