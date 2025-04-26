using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Account.ForgetPassword
{
    public class ForgetPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public ForgetPasswordCommand(string email)
        {
            Email = email;
        }
    }
}
