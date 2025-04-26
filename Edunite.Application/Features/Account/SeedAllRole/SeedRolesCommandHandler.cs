using Edunite.Domain.Features.IRoleRepo;
using Edunite.DTO.Features.UserAuth.UserRole;

namespace Edunite.Application.Features.Account.SeedAllRole
{
    public class SeedRolesCommandHandler : IRequestHandler<SeedRolesCommand, Unit>
    {
        private readonly IRoleRepository _roleRepository;

        public SeedRolesCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(SeedRolesCommand request, CancellationToken cancellationToken)
        {
            var roles = new[]
        {
            StaticUserRole.Admin,
            StaticUserRole.Teacher,
            StaticUserRole.User
        };

            foreach (var role in roles)
            {
                if (!await _roleRepository.RoleExistsAsync(role))
                {
                    await _roleRepository.CreateRoleAsync(role);
                }
            }
            return Unit.Value;
        }
    }
}
