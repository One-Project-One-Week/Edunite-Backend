using Edunite.Domain.Features.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Account.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IAccount _accountService;
        public ResetPasswordCommandHandler(IAccount accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _accountService.ResetPasswordAsync(request.ResetPasswordDto);
            return Unit.Value;
        }
    }
}
