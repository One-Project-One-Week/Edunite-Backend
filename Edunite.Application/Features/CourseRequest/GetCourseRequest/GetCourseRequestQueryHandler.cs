namespace Edunite.Application.Features.CourseRequest.GetCourseRequest;

public class GetCourseRequestQueryHandler : IRequestHandler<GetCourseRequestQuery, Result<CourseRequestListModel>>
{
	private readonly ICourseRequestRepository _courseRequestRepository;

	public GetCourseRequestQueryHandler(ICourseRequestRepository courseRequestRepository)
	{
		_courseRequestRepository = courseRequestRepository;
	}

	#region Handle

	public async Task<Result<CourseRequestListModel>> Handle(GetCourseRequestQuery request,  CancellationToken cancellationToken)
	{
		Result<CourseRequestListModel> result;

		if(request.PageNo <= 0)
		{
			result = Result<CourseRequestListModel>.Fail(MessageResource.InvalidPageNo);
			goto result;
		}

		if (request.PageSize <= 0)
		{
			result = Result<CourseRequestListModel>.Fail(MessageResource.InvalidPageSize);
			goto result;
		}

		result = await _courseRequestRepository.GetCourseRequestListAsync(request.PageNo, request.PageSize, cancellationToken);

		result:
		return result;
	}

	#endregion
}
