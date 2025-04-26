using Edunite.Application.Extension.PasswordHah;
using Edunite.DTO.Features.UserAuth;
namespace Edunite.Infrastructure.Features.User
{
	public class UserRepository : IUserRepository
	{
        private readonly AppDbContext _context;
		private readonly IPasswordHasher _passwordHasher;

		public UserRepository(AppDbContext appDbContext, IPasswordHasher passwordHasher)
		{
			_context = appDbContext;
            _passwordHasher = passwordHasher;
        }

		public async Task DeletePasswordResetTokenAsync(AspNetUser user)
		{
			var existingUser = await _context.AspNetUsers
				  .FirstOrDefaultAsync(u => u.Id == user.Id);

			if (existingUser == null)
			{
				throw new Exception("User not found");
			}

			existingUser.ForgetPasswordToken = null;
			existingUser.ForgetPasswordTokenExpiryTime = default;

			_context.AspNetUsers.Update(existingUser);
			await _context.SaveChangesAsync();
		}

		public async Task<AuthUserModel> GetByRefreshTokenAsync(string token)
		{
			try
			{
				var RefreshTokenUser = await _context.AspNetUsers
			   .FirstOrDefaultAsync(u => u.RefreshToken == token);
				if (RefreshTokenUser == null)
				{
					throw new Exception("User not found");
				}
				var user = new AuthUserModel
				{
					Id = RefreshTokenUser.Id,
					Name = RefreshTokenUser.Name,
					Email = RefreshTokenUser.Email,
					PhoneNumber = RefreshTokenUser.PhoneNumber,
					RefreshToken = RefreshTokenUser.RefreshToken,
					RefreshTokenExpiryTime = RefreshTokenUser.RefreshTokenExpiryTime
				};
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception("Error retrieving user by refresh token", ex);
			}
		}

		public async Task<string> GetValidPasswordResetTokenByEmailAsync(string email)
		{
			try
			{
				var user = await _context.AspNetUsers
	  .FirstOrDefaultAsync(u => u.Email == email);

				if (user == null)
					throw new Exception("User not found");

				var token = user.ForgetPasswordToken;
				if (token == null)
				{
					throw new Exception("Token not found");
				}
				return token;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.Message}", ex);
			}
		}

		public async Task UpdateTokenAsync(string email, string resetPasswordToken)
		{
			var user = await _context.AspNetUsers
                  .FirstOrDefaultAsync(u => u.Email == email);
			if(user == null)
			{
                throw new Exception("User not found");
            }
                user.ForgetPasswordToken = resetPasswordToken;
			user.ForgetPasswordTokenExpiryTime = DateTime.UtcNow.AddMinutes(15);

			_context.AspNetUsers.Update(user);
			await _context.SaveChangesAsync();
		}
        public async Task UpdatePasswordAsync(Guid userId, string newPassword)
        {
            var user = await _context.AspNetUsers.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found.");
            user.PasswordHash = _passwordHasher.HashPassword(newPassword);
            await _context.SaveChangesAsync();
        }

	
	}
}

      