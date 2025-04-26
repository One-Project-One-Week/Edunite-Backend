using Edunite.Application.Features.CourseRequest.GetCourseRequestByStudentId;

namespace Edunite.Application.Features.CourseRequest.GetCourseRequestByTeacherId;

public class GetCourseRequestByTeacherIdQueryHandler : IRequestHandler<GetCourseRequestByTeacherIdQuery, Result<List<CourseRequestModel>>>
{
	private readonly ICourseRequestRepository _courseRequestRepository;

	public GetCourseRequestByTeacherIdQueryHandler(ICourseRequestRepository courseRequestRepository)
	{
		_courseRequestRepository = courseRequestRepository;
	}

	public async Task<Result<List<CourseRequestModel>>> Handle(GetCourseRequestByTeacherIdQuery request, CancellationToken cancellationToken)
	{
		return await _courseRequestRepository.GetCourseRequestsByStudentIdAsync(request.TeacherUserId, cancellationToken);
	}
}