using Edunite.Application.Extension.RoleAuth;
namespace Edunite.Application.Features.Account.SeedRoleToTeacher
{
    public class SeedRoleToTeacherCommand : IRequest<Unit>, IRequireRoles
    {
        public string Email { get; set; }
        public SeedRoleToTeacherCommand(string email)
        {
            Email = email;
        }

        public string[] Roles => new string[] { "Admin" };
    }
}
