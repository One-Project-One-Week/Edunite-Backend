using Edunite.Application.Features.CourseRequest.CreateCourseRequest;
using Edunite.Application.Features.CourseRequest.GetCourseRequestByStudentId;
using Edunite.Application.Features.CourseRequest.GetCourseRequestByTeacherId;
using Edunite.DTO.Features.CourseRequest;

namespace Edunite.Presentation.Controllers.CourseRequest;

[Route("api/[controller]")]
[ApiController]
public class CourseRequestController : BaseController
{
	private readonly IMediator _mediator;

	public CourseRequestController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> GetCourseRequestListAsync(int pageNo, int pageSize, CancellationToken cancellationToken)
	{
		var query = new GetStudentListQuery(pageNo, pageSize);
		var result = await _mediator.Send(query, cancellationToken);

		return Content(result);
	}

	[HttpGet("student/{studentUserId}")]
	public async Task<IActionResult> GetCourseRequestsByStudentId(Guid studentUserId, CancellationToken cancellationToken)
	{
		var query = new GetCourseRequestByStudentIdQuery(studentUserId);
		var result = await _mediator.Send(query, cancellationToken);

		return Content(result);
	}


	[HttpGet("student/{teacherUserId}")]
	public async Task<IActionResult> GetCourseRequestsByTeacherId(Guid teacherUserId, CancellationToken cancellationToken)
	{
		var query = new GetCourseRequestByTeacherIdQuery(teacherUserId);
		var result = await _mediator.Send(query, cancellationToken);

		return Content(result);
	}

	[HttpPost("create")]
	public async Task<IActionResult> CreateCourseRequestAsync([FromBody] CreateCourseRequestModel model, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new CreateCourseRequestCommand(model), cancellationToken);
		if (!result.IsSuccess)
			return StatusCode((int)result.StatusCode, result);

		return Ok(result);
	}

}
