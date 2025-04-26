namespace Edunite.Application.Features.Student.GetStudentById;

#region GetStudentByIdQueryHandler

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentModel>>
{
	private readonly IStudentRepository _studentRepository;

	public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
	{
		_studentRepository = studentRepository;
	}

	#region Handle

	public async Task<Result<StudentModel>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
	{
		Result<StudentModel> result;

		if (request.Id == Guid.Empty)
		{
			result = Result<StudentModel>.Fail("Invalid student ID.");
			goto result;
		}
		result = await _studentRepository.GetStudentByIdAsync(request.Id, cancellationToken);

	result:
		return result;
	}

	#endregion
}

#endregion