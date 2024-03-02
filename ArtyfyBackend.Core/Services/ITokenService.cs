using ArtyfyBackend.Core.Models.Token;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.Services
{
    public interface ITokenService
    {
        TokenModel CreateToken(UserApp userApp, IList<string> userRoles);
    }
}