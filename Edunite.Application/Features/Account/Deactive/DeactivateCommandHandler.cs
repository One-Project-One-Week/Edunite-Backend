using Edunite.Domain.Features.Account;

namespace Edunite.Application.Features.Account.Deactive
{
    public class DeactiveCommandHandler : IRequestHandler<DeactivateCommand, Unit>
    {
        private readonly IAccount _accountService;
        public DeactiveCommandHandler(IAccount accountService)
        {
            _accountService = accountService;
        }
        public async Task<Unit> Handle(DeactivateCommand request, CancellationToken cancellationToken)
        {
            await _accountService.DeactivateAsync(request.DeactivateDto);
            return Unit.Value;
        }
    }
}
