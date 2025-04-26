using Edunite.DTO.Features.Account.Requests;
using Edunite.DTO.Features.Account.Response;

namespace Edunite.Domain.Features.Account
{
    public interface IAccount
    {
        Task RegisterAsync(RegisterDto request);
        Task<LoginResponseDto> LoginAsync(LoginDto request);
        Task<LoginResponseDto> RefreshTokenAsync(string? refreshToken);
        Task ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task ForgetPasswordAsync(string email);
        Task DeactivateAsync(DeactivateDto deactivate);
        Task SeedRoleToAdminAsync(string email);
        Task SeedRoleToTeacherAsync(string email);
    }
}
