using Edunite.Domain.Features.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Account.Register
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, Unit>
    {
        private readonly IAccount _accountService;
        public UserRegisterCommandHandler(IAccount accountService)
        {
            _accountService = accountService;
        }
        public async Task<Unit> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            await _accountService.RegisterAsync(request.RegisterDto);
            return Unit.Value;
        }
    }
}
