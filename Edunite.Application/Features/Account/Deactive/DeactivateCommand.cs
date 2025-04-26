using Edunite.DTO.Features.Account.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Account.Deactive
{
    public class DeactivateCommand : IRequest<Unit>
    {
        public DeactivateDto DeactivateDto { get; set; }
        public DeactivateCommand(DeactivateDto deactivateDto)
        {
            DeactivateDto = deactivateDto;
        }
    }
}
