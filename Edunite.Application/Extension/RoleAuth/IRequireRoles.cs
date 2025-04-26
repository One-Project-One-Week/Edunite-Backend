using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Extension.RoleAuth
{
    public interface IRequireRoles
    {
        string[] Roles { get; }
    }
}
