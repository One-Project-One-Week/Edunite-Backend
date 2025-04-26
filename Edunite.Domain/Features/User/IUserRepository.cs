using Edunite.DbService.AppDbContextModels;
using Edunite.DTO.Features.UserAuth;
public interface IUserRepository
{
    Task UpdatePasswordAsync(Guid userId, string newPassword);
    Task<AuthUserModel> GetByRefreshTokenAsync(string token);
    Task<string> GetValidPasswordResetTokenByEmailAsync(string email);
    Task DeletePasswordResetTokenAsync(AspNetUser user);
    Task UpdateTokenAsync(string email, string resetPasswordToken);

}


