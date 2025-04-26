
using Edunite.Application.Extension.RoleAuth;

namespace Edunite.Application.Features.Account.SeedRoleToAdmin
{
    public class SeedRoleToAdminCommand : IRequest<Unit>, IRequireRoles
    {
        public string Email { get; set; }
        public SeedRoleToAdminCommand(string email)
        {
            Email = email;
        }

        public string[] Roles => new string[] { "Admin" };
    }
}
