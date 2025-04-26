namespace Edunite.Application.Features.Student.GetStudentById;

#region GetStudentByIdQuery

public class GetStudentByIdQuery : IRequest<Result<StudentModel>>
{
	public Guid Id { get; set; }
	public GetStudentByIdQuery(Guid userId)
	{
		Id = userId;
	}
}

#endregion