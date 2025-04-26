namespace Edunite.DTO.Features.Student;

#region StudentModel

public class StudentModel
{
	//public Guid StudentId { get; set; }
	public string? Grade { get; set; }
	public string? Interests { get; set; }
	public string? EnrollCourses { get; set; }
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public string Email { get; set; }
	public string? PhoneNumber { get; set; }
}

#endregion
