namespace Edunite.Application.Features.Student.CreateStudent;

#region CreateStudentCommand

public class CreateStudentCommand : IRequest<Result<StudentModel>>
{
	public StudentRequestModel requestModel { get; set; }

	public CreateStudentCommand(StudentRequestModel requestModel)
	{
		this.requestModel = requestModel;
	}
}

#endregion
