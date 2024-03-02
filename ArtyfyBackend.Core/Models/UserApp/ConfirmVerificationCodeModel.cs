namespace ArtyfyBackend.Core.Models.UserApp
{
    public class ConfirmVerificationCodeModel
    {
        public string UserId { get; set; }
        public string VerificationCode { get; set; }
    }
}