using Edunite.Domain.Features.Course;
using Edunite.Domain.Features.CourseRequest;

namespace Edunite.Application.Features.CourseRequest.CreateCourseRequest;

public class CreateCourseRequestCommandHandler : IRequestHandler<CreateCourseRequestCommand, Result<Guid>>
{
	private readonly ICourseRequestRepository _courseRepository;

	public CreateCourseRequestCommandHandler(ICourseRequestRepository courseRepository)
	{
		_courseRepository = courseRepository;
	}

	public async Task<Result<Guid>> Handle(CreateCourseRequestCommand request, CancellationToken cancellationToken)
	{
		return await _courseRepository.CreateCourseRequestAsync(request.Model, cancellationToken);
	}
}
