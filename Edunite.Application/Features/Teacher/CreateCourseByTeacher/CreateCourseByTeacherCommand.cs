namespace Edunite.Application.Features.Teacher.CreateCourseByTeacher;

#region CreateCourseByTeacherCommand

public class CreateCourseByTeacherCommand : IRequest<Unit>, IRequireRoles
{
    public string[] Roles => new string[] { "Teacher" };
    public TeacherCourseCreatingDTO TeacherRequest { get; set; }
    public CreateCourseByTeacherCommand(TeacherCourseCreatingDTO createCourseDto)
    {
        TeacherRequest = createCourseDto;
    }
}

#endregion
