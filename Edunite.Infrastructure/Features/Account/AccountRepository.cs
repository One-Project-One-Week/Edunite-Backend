using Edunite.Application.Extension.PasswordHah;
using Edunite.Domain.Features.Account;
using Edunite.Domain.Features.AuthProcessor;
using Edunite.Domain.Features.SMTPRepo;
using Edunite.Domain.Features.TokenProcessors;
using Edunite.DTO.Features.Account.Requests;
using Edunite.DTO.Features.Account.Response;
using Edunite.DTO.Features.User;
using Edunite.DTO.Features.UserAuth;
using Edunite.DTO.Features.UserAuth.UserRole;

namespace Edunite.Infrastructure.Features.Account
{
    public class AccountRepository : IAccount
    {
        private readonly IAuthTokenProcessor _authTokenProcessor;
        private readonly ISMTPEmail _smtpEmailService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProcessor _tokenProcessor;
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public AccountRepository(IAuthTokenProcessor authTokenProcessor,  
            ISMTPEmail smtpEmail, IUserRepository userRepository, ITokenProcessor tokenProcessor,AppDbContext context, IPasswordHasher password)
        {
            _authTokenProcessor = authTokenProcessor;
            _smtpEmailService = smtpEmail;
            _userRepository = userRepository;
            _tokenProcessor = tokenProcessor;
            _context = context;
            _passwordHasher = password;
        }

        public async Task DeactivateAsync(DeactivateDto deactivate)
        {
            
            try
            {
                var email = deactivate.Email;
                var user = await _context.AspNetUsers.FindAsync(email);
                if (user == null )
                {
                    throw new Exception("Invalid email or password");
                }
                user.IsActive = false;
                _context.Update(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task ForgetPasswordAsync(string email)
        {
            try
            {
                var resetToken = _tokenProcessor.GenerateOTPToken();
                await _userRepository.UpdateTokenAsync(email, resetToken);

                var subject = "Reset Your Password";
                var body = $"Here is your password reset token: {resetToken}";

                await _smtpEmailService.SentPasswordAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto request)
        {
            try
            {
                var email = request.Email;
                var user = await _context.AspNetUsers.FindAsync(email);
                if (user == null )
                {
                    throw new Exception("Invalid email or password");
                }

                if (!user.IsActive)
                {
                    throw new Exception("User is deactivated");
                }
                var roles = await (from ur in _context.AspNetRoles
                                   join r in _context.AspNetUsers on ur.Id equals r.Id
                                   where ur.Id == user.Id
                                   select r.Name).ToListAsync();

                AuthUserModel authUser = new AuthUserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    IsActive = user.IsActive
                };
                var (token, expireTime) = _authTokenProcessor.GenerateToken(authUser, roles);
                var refreshToken = _tokenProcessor.GenerateRefreshToken();
                var refreshTokenExpireTime = DateTime.UtcNow.AddDays(7);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = refreshTokenExpireTime;

                _context.AspNetUsers.Update(user);
                _context.SaveChanges();

                _authTokenProcessor.WriteTokenInHttpOnlyCookie("refresh_token", refreshToken, refreshTokenExpireTime);
                var Result = new LoginResponseDto
                {
                    AccessToken = token,
                    ExpireAt = expireTime,
                };
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(string? refreshToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    throw new Exception("Refresh token is empty or missing");
                }
                
                var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);
                if (user == null)
                {
                    throw new Exception("Unable to find user with this refresh token");
                }

                if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    throw new Exception("Refresh token is expired");
                }
                var Netuser= await _context.AspNetUsers.FindAsync(user.Id);
                if(Netuser == null)
                {
                    throw new Exception("User not found");
                }
                var roles = await (from ur in _context.AspNetRoles
                                   join r in _context.AspNetUsers on ur.Id equals r.Id
                                   where ur.Id == user.Id
                                   select r.Name).ToListAsync();
                var (token, expireTime) = _authTokenProcessor.GenerateToken(user, roles);
                var newRefreshToken = _tokenProcessor.GenerateRefreshToken();
                var refreshTokenExpireTime = DateTime.UtcNow.AddDays(7);
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = refreshTokenExpireTime;

                _context.AspNetUsers.Update(Netuser);
                _context.SaveChanges();
                _authTokenProcessor.WriteTokenInHttpOnlyCookie("refresh_token", newRefreshToken, refreshTokenExpireTime);//token that we created above
                var Result = new LoginResponseDto
                {
                    AccessToken = token,
                    ExpireAt = expireTime,
                };
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task RegisterAsync(RegisterDto request)
        {
            var email = request.Email;
            var Existinguser = await _context.AspNetUsers.FindAsync(email);
            if (Existinguser != null)
            {
                throw new Exception("User already exists");
            }

            var newUser = new AspNetUser(
                request.Name,
                request.Email
            );

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser.PasswordHash);

            // 1. Save the new user
            await _context.AspNetUsers.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // 2. Find the RoleId based on StaticUserRole.User
            var role = await _context.AspNetRoles
                .FirstOrDefaultAsync(r => r.Name == StaticUserRole.User);

            if (role == null)
                throw new Exception($"Role '{StaticUserRole.User}' not found.");

            // 3. Insert into UserRoles table manually
            var userRole = new AspNetUserRole
            {
                UserId = newUser.Id,
                RoleId = role.Id
            };

			_context.AspNetUserRoles.Add(userRole);
            await _context.SaveChangesAsync();

        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            var OTP = await _userRepository.GetValidPasswordResetTokenByEmailAsync(resetPassword.Email);
            if (OTP == null)
            {
                throw new Exception("Invalid or expired OTP token");
            }
            var email = resetPassword.Email;
            var Existinguser = await _context.AspNetUsers.FindAsync(email);
            if (Existinguser == null)
            {
                throw new Exception("User not found");
            }
            Existinguser.PasswordHash = _passwordHasher.HashPassword(resetPassword.Password);
            await _userRepository.UpdatePasswordAsync(Existinguser.Id, Existinguser.PasswordHash);
            await _userRepository.DeletePasswordResetTokenAsync(Existinguser);
        }

        public async Task SeedRoleToAdminAsync(string email)
        {
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new Exception($"{email} is not registered.");
            }

            var userRoles = await _context.AspNetUserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            var adminRole = await _context.AspNetRoles
                .FirstOrDefaultAsync(r => r.Name == StaticUserRole.Admin);

            if (adminRole == null)
            {
                throw new Exception($"Admin role does not exist.");
            }

            if (userRoles.Contains(adminRole.Id))
            {
                throw new Exception($"{email} is already an admin.");
            }

            _context.AspNetUserRoles.Add(new AspNetUserRole
            {
                UserId = user.Id,
                RoleId = adminRole.Id
            });

            await _context.SaveChangesAsync();
        }

        public async Task SeedRoleToTeacherAsync(string email)
        {
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new Exception($"{email} is not registered.");
            }

            var teacherRole = await _context.AspNetRoles
                .FirstOrDefaultAsync(r => r.Name == StaticUserRole.Teacher);

            if (teacherRole == null)
            {
                throw new Exception("Teacher role does not exist.");
            }

            var alreadyInRole = await _context.AspNetUserRoles
                .AnyAsync(ur => ur.UserId == user.Id && ur.RoleId == teacherRole.Id);

            if (alreadyInRole)
            {
                throw new Exception($"{email} is already a teacher.");
            }

            _context.AspNetUserRoles.Add(new AspNetUserRole
            {
                UserId = user.Id,
                RoleId = teacherRole.Id
            });

            await _context.SaveChangesAsync();
        }
    }
}
