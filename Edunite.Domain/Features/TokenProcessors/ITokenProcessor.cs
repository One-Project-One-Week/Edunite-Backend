using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Domain.Features.TokenProcessors
{
    public interface ITokenProcessor
    {
        string GenerateOTPToken();
        string GenerateRefreshToken();
    }
}
