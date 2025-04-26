using Edunite.DTO.Features.Account.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Account.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Unit>
    {
        public ResetPasswordDto ResetPasswordDto { get; set; }
        public ResetPasswordCommand(ResetPasswordDto resetPasswordDto)
        {
            ResetPasswordDto = resetPasswordDto;
        }
    }
}
