using Edunite.Domain.Features.Account;

namespace Edunite.Application.Features.Account.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Unit>
    {
        private readonly IAccount _accountService;
        public ForgetPasswordCommandHandler(IAccount accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _accountService.ForgetPasswordAsync(request.Email);
            return Unit.Value;
        }
    }
}
