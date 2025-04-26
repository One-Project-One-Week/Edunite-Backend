using Edunite.Domain.Features.Account;

namespace Edunite.Application.Features.Account.SeedRoleToAdmin
{
    public class SeedRoleToAdminCommandHandler : IRequestHandler<SeedRoleToAdminCommand, Unit>
    {
        private readonly IAccount _accountService;
        public SeedRoleToAdminCommandHandler(IAccount accountService)
        {
            _accountService = accountService;
        }
        public async Task<Unit> Handle(SeedRoleToAdminCommand request, CancellationToken cancellationToken)
        {
            await _accountService.SeedRoleToAdminAsync(request.Email);
            return Unit.Value;
        }
    }
}
