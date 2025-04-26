namespace Edunite.Application.Features.Student.CreateStudent;

#region CreateStudentCommandHandler

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<StudentModel>>
{

	private readonly IStudentRepository _studentRepository;

	public CreateStudentCommandHandler(IStudentRepository studentRepository)
	{
		_studentRepository = studentRepository;
	}

	#region Handle

	public async Task<Result<StudentModel>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
	{
		Result<StudentModel> result;

		try
		{
			var model = request.requestModel;

			if (model.UserId is null || model.UserId == Guid.Empty)
				result =  Result<StudentModel>.Fail("UserId is required.");

			if (string.IsNullOrWhiteSpace(model.Grade))
				result = Result<StudentModel>.Fail("Grade is required.");

			if (string.IsNullOrWhiteSpace(model.EnrollCourses))
				result = Result<StudentModel>.Fail("EnrollCourses is required.");

			result =  await _studentRepository.CreateStudentAsync(model, cancellationToken);
		}
		catch (Exception ex)
		{
			result = Result<StudentModel>.Failure(ex);
		}

	result:
		return result;

	}

	#endregion
}

#endregion