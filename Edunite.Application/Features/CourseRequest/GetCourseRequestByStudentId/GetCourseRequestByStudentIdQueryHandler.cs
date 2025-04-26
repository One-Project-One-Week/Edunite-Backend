namespace Edunite.Application.Features.CourseRequest.GetCourseRequestByStudentId;

#region GetCourseRequestByStudentIdQueryHandler

public class GetCourseRequestByStudentIdQueryHandler : IRequestHandler<GetCourseRequestByStudentIdQuery, Result<List<CourseRequestModel>>> 
{
	private readonly ICourseRequestRepository _courseRequestRepository;

	public GetCourseRequestByStudentIdQueryHandler(ICourseRequestRepository courseRequestRepository)
	{
		_courseRequestRepository = courseRequestRepository;
	}

	public async Task<Result<List<CourseRequestModel>>> Handle(GetCourseRequestByStudentIdQuery request, CancellationToken cancellationToken)
	{
		return await _courseRequestRepository.GetCourseRequestsByStudentIdAsync(request.StudentUserId, cancellationToken);
	}
}

#endregion