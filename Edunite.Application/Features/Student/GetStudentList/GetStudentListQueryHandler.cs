namespace Edunite.Application.Features.Student.GetStudentList;

public class GetStudentListQueryHandler : IRequestHandler<GetStudentListQuery, Result<StudentListModel>>
{
	private readonly IStudentRepository _studentRepository;

	public GetStudentListQueryHandler(IStudentRepository studentRepository)
	{
		_studentRepository = studentRepository;
	}

	#region Handle

	public async Task<Result<StudentListModel>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
	{
		Result<StudentListModel> result;

		if (request.PageNo <= 0)
		{
			result = Result<StudentListModel>.Fail(MessageResource.InvalidPageNo);
			goto result;
		}

		if (request.PageSize <= 0)
		{
			result = Result<StudentListModel>.Fail(MessageResource.InvalidPageSize);
			goto result;
		}

		result = await _studentRepository.GetStudentListAsync(request.PageNo, request.PageSize, cancellationToken);

	result:
		return result;
	}

	#endregion

}
