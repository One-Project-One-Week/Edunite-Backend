using Edunite.Domain.Features.TokenProcessors;
using System.Security.Cryptography;

namespace Edunite.Infrastructure.Features.TokenRepo
{
    public class TokenProcessor : ITokenProcessor
    {
        public string GenerateOTPToken()
        {
            int code = RandomNumberGenerator.GetInt32(100000, 1000000);
            return code.ToString();
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
