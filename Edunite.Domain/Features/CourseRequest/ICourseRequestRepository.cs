using Edunite.DTO.Features.CourseRequest;

namespace Edunite.Domain.Features.CourseRequest;

public interface ICourseRequestRepository
{
	Task<Result<CourseRequestListModel>> GetCourseRequestListAsync(int pageNo, int pageSize, CancellationToken cancellationToken);
	Task<Result<List<CourseRequestModel>>> GetCourseRequestsByStudentIdAsync(Guid studentUserId, CancellationToken cancellationToken);
	Task<Result<List<CourseRequestModel>>> GetCourseRequestsByTeacherIdAsync(Guid teacherUserId, CancellationToken cancellationToken);
	Task<Result<Guid>> CreateCourseRequestAsync(CreateCourseRequestModel model, CancellationToken cancellationToken);

}
