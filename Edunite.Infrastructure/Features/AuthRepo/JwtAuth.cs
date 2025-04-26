using Edunite.Domain.Features.AuthProcessor;
using Edunite.DTO.Features.UserAuth;
using Edunite.DTO.Features.UserAuth.JWTPrincipal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Edunite.Infrastructure.Features.AuthRepo
{
	public class JwtAuth : IAuthTokenProcessor
    {
        private readonly Jwt _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        public JwtAuth(IOptions<Jwt> jwtOption, IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _jwtOptions = jwtOption.Value;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public (string token, DateTime expireTime) GenerateToken(AuthUserModel user, IList<string> roles)
        {
            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credential = new SigningCredentials(
                signingkey,
                SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.ToString()),
            };
            var User = _context.AspNetUsers.FirstOrDefault(x => x.Id == user.Id);
            var userRole = _context.AspNetRoles
                .Where(x => x.Id == User.Id)
                .Select(x => x.Name)
                .ToListAsync();
            foreach (var role in userRole.Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireTime);
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credential
            );
            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenHandler, expires);
        }

        public void WriteTokenInHttpOnlyCookie(string cookieName, string token, DateTime expireTime)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = expireTime,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.Strict
                });
        }
    }
}
