namespace Edunite.DTO.Features.Student;

#region StudentRequestModel

public class StudentRequestModel
{
	public Guid StudentId { get; set; }

	public Guid? UserId { get; set; }

	public string? Grade { get; set; }

	//public string? Interests { get; set; }

	public string? EnrollCourses { get; set; }

	//public bool? IsActive { get; set; }
}

#endregion
