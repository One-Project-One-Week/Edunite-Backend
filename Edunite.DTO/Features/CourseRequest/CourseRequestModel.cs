﻿namespace Edunite.DTO.Features.CourseRequest;

#region CourseRequestModel

public class CourseRequestModel
{
	public Guid CourseRequestId { get; set; }

	public Guid? UserId { get; set; }

	public Guid? CourseId { get; set; }

	public Guid? TeacherDetailId { get; set; }

	public string? Status { get; set; }

}

#endregion
