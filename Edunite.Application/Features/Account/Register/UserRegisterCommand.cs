using Edunite.DTO.Features.Account.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Account.Register
{
    public class UserRegisterCommand : IRequest<Unit>
    {
        public RegisterDto RegisterDto { get; set; }
        public UserRegisterCommand(RegisterDto registerDto)
        {
            RegisterDto = registerDto;
        }
    }
}
