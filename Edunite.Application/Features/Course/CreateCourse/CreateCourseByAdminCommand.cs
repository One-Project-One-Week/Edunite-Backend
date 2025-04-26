using Edunite.Application.Extension.RoleAuth;
using Edunite.DTO.Features.Course.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.Application.Features.Course.CreateCourse
{
    public class CreateCourseByAdminCommand : IRequest<Unit>, IRequireRoles
    {
        public CreateCourseDto CreateCourseDto { get; set; }

        public string[] Roles => new string[] { "Admin" };

        public CreateCourseByAdminCommand(CreateCourseDto createCourseDto)
        {
            CreateCourseDto = createCourseDto;
        }
    }
}
