namespace Edunite.Application.Features.Student.DeleteStudent;

#region DeleteStudentCommandHandler

public class DeleteStudentCommandHandler :IRequestHandler<DeleteStudentCommand, Result<StudentModel>>
{
	private readonly IStudentRepository _studentRepository;

	public DeleteStudentCommandHandler(IStudentRepository studentRepository)
	{
		_studentRepository = studentRepository;
	}

	#region Handle

	public async Task<Result<StudentModel>> Handle(DeleteStudentCommand request,  CancellationToken cancellationToken)
	{
		Result<StudentModel> result;

		if(request.StudentId == Guid.Empty)
		{
			result = Result<StudentModel>.Fail(MessageResource.InvalidId);
			goto result;
		}

		result = await _studentRepository.DeactivateStudentAsync(request.StudentId, cancellationToken);

		result:
		return result;
	}

	#endregion
}

#endregion
