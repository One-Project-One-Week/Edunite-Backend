namespace Edunite.Application.Features.Student.DeleteStudent;

#region DeleteStudentCommand

public class DeleteStudentCommand : IRequest<Result<StudentModel>>
{
	public Guid StudentId { get; set; }	

	public DeleteStudentCommand(Guid studentId)
	{
		StudentId = studentId;
	}	
}

#endregion
