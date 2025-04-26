using Edunite.Domain.Features.Account;
namespace Edunite.Application.Features.Account.SeedRoleToTeacher
{
    public class SeedRoleToTeacherCommandHandler : IRequestHandler<SeedRoleToTeacherCommand, Unit>
    {
        private readonly IAccount _accountService;
        public SeedRoleToTeacherCommandHandler(IAccount accountService)
        {
            _accountService = accountService;
        }
        public async Task<Unit> Handle(SeedRoleToTeacherCommand request, CancellationToken cancellationToken)
        {
            await _accountService.SeedRoleToTeacherAsync(request.Email);
            return Unit.Value;
        }
    }
}
