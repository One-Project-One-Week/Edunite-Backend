using Edunite.Domain.Features.Account;
using Edunite.DTO.Features.Account.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Account.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IAccount _accountService;
        public LoginCommandHandler(IAccount accountService)
        {
            _accountService = accountService;
        }
        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await _accountService.LoginAsync(request.LoginDto);
            return token;
        }
    }
}
