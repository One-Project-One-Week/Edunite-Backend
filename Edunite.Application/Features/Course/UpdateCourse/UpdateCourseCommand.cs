using Edunite.Application.Extension.RoleAuth;
using Edunite.DTO.Features.Course.responses;
namespace Edunite.Application.Features.Course.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<Unit>, IRequireRoles
    {
        public GetAllCourseDto UpdateCourse { get; set; }

        public string[] Roles => new string[] { "Admin", "Teacher" };

        public UpdateCourseCommand(GetAllCourseDto updateCourse)
        {
            UpdateCourse = updateCourse;
        }
    }
}
