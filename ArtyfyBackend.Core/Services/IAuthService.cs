using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Login;
using ArtyfyBackend.Core.Models.Register;
using ArtyfyBackend.Core.Models.Token;
using ArtyfyBackend.Core.Models.UserApp;
using ArtyfyBackend.Core.Responses;

namespace ArtyfyBackend.Core.Services
{
    public interface IAuthService
    {
        Task<Response<TokenModel>> LoginAsync(LoginModel loginModel);
        Task<Response<UserAppModel>> RegisterAsync(RegisterModel registerModel);
        Task<Response<UpdatePasswordModel>> UpdatePasswordAsync(UpdatePasswordModel updatePasswordModel);

        Task<Response<NoDataModel>> SendVerificationCodeForResetPasswordAsync(string email);

        Task<Response<NoDataModel>> ConfirmVerificationCodeForResetPasswordAsync(ConfirmVerificationCodeResetPasswordModel model);

        Task<Response<NoDataModel>> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);

        Task<Response<NoDataModel>> SendVerificationCode(string userId);

        Task<Response<NoDataModel>> ConfirmVerificationCode(ConfirmVerificationCodeModel confirmVerificationCodeModel);

        Task<Response<List<UserAppModel>>> GetAllUsersAsync();

        Task<Response<List<UserAppModel>>> GetUsersByRoleAsync(string role);

        Task<Response<UserAppModel>> GetUserByIdAsync(string userId);

        Task<Response<UserAppUpdateModel>> UpdateUserAsync(UserAppUpdateModel userAppUpdateModel);
    }
}