using Edunite.DTO.Features.Account.Requests;
using Edunite.DTO.Features.Account.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Account.Login
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public LoginDto LoginDto { get; set; }
        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto;
        }
    }
}
