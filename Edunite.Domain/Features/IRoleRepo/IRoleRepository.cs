
namespace Edunite.Domain.Features.IRoleRepo
{
    public interface IRoleRepository
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task CreateRoleAsync(string roleName);
    }
}
