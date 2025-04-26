namespace Edunite.Application.Features.CourseRequest.GetCourseRequestByTeacherId;

public class GetCourseRequestByTeacherIdQuery : IRequest<Result<List<CourseRequestModel>>>
{
	public Guid TeacherUserId { get; set; }

	public GetCourseRequestByTeacherIdQuery(Guid teacherUserId)
	{
		TeacherUserId = teacherUserId;
	}
}

