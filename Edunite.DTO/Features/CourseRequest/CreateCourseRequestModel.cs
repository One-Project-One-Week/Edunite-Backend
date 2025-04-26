namespace Edunite.DTO.Features.CourseRequest;

#region CreateCourseRequestModel

public class CreateCourseRequestModel
{
	public Guid CourseRequestId { get; set; }
	public Guid? UserId { get; set; }

	public Guid? CourseId { get; set; }

	public Guid? TeacherDetailId { get; set; }

	public string? Status { get; set; }

}

#endregion
