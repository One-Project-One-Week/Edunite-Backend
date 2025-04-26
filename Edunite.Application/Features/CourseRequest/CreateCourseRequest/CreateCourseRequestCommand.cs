using Edunite.DTO.Features.CourseRequest;

namespace Edunite.Application.Features.CourseRequest.CreateCourseRequest;

public class CreateCourseRequestCommand : IRequest<Result<Guid>>
{
	public CreateCourseRequestModel Model { get; set; }
	public CreateCourseRequestCommand(CreateCourseRequestModel model)
	{
		Model = model;
	}
}