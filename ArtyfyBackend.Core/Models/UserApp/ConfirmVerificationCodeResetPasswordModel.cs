namespace ArtyfyBackend.Core.Models.UserApp
{
    public class ConfirmVerificationCodeResetPasswordModel
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}