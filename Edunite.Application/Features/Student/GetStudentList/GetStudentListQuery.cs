namespace Edunite.Application.Features.Student.GetStudentList;

#region GetStudentListQuery

public class GetStudentListQuery : IRequest<Result<StudentListModel>>
{
	public int PageNo { get; set; }
	public int PageSize { get; set; }
	public GetStudentListQuery(int pageNo, int pageSize)
	{
		PageNo = pageNo;
		PageSize = pageSize;
	}
}

#endregion
