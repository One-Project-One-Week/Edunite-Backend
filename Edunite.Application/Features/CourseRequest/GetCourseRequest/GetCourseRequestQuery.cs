using Edunite.DTO.Features.CourseRequest;

namespace Edunite.Application.Features.CourseRequest.GetCourseRequest;

public class GetCourseRequestQuery : IRequest<Result<CourseRequestListModel>>
{

	public int PageNo { get; set; }
	public int PageSize { get; set; }

	public GetCourseRequestQuery(int pageNo, int pageSize)
	{
		PageNo = pageNo;
		PageSize = pageSize;
	}
}
