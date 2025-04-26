namespace Edunite.Application.Features.CourseRequest.GetCourseRequestByStudentId;

#region GetCourseRequestByStudentIdQuery

public class GetCourseRequestByStudentIdQuery : IRequest<Result<List<CourseRequestModel>>>
{
	public Guid StudentUserId { get; set; }

	public GetCourseRequestByStudentIdQuery(Guid studentUserId)
	{
		StudentUserId = studentUserId;
	}
}

#endregion