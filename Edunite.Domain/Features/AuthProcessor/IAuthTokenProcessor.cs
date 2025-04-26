using Edunite.DTO.Features.UserAuth;

namespace Edunite.Domain.Features.AuthProcessor
{
    public interface IAuthTokenProcessor
    {
        (string token, DateTime expireTime) GenerateToken(AuthUserModel user, IList<string> roles);
        void WriteTokenInHttpOnlyCookie(string cookieName, string token, DateTime expireTime);
    }
}
