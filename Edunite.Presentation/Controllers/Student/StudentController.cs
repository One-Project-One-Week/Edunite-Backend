namespace Edunite.Presentation.Controllers.Student;

[Route("api/student")]
[ApiController]
public class StudentController : BaseController
{
	private readonly IMediator _mediator;

	public StudentController(IMediator mediator)
	{
		_mediator = mediator;
	}

	#region GetStudentListAsync

	[HttpGet]
	public async Task<IActionResult> GetStudentListAsync(int pageNo, int pageSize, CancellationToken cancellationToken)
	{
		var query = new GetStudentListQuery(pageNo, pageSize);
		var result = await _mediator.Send(query, cancellationToken);

		return Content(result);
	}

	#endregion

	#region GetStudentByIdAsync

	[HttpGet("id")]
	public async Task<IActionResult> GetStudentByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var query = new GetStudentByIdQuery(id);
		var result = await _mediator.Send(query, cancellationToken);

		return Content(result);
	}

	#endregion

	#region CreateStudentAsync

	[HttpPost]
	public async Task<IActionResult> CreateStudentAsync([FromBody] StudentRequestModel requestModel,CancellationToken cancellationToken)
	{
		var command = new CreateStudentCommand(requestModel);
		var result = await _mediator.Send(command,cancellationToken);

		return Content(result);	
	}

	#endregion

	#region DeactivateStudentAsync

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeactivateStudentAsync(Guid id, CancellationToken cancellationToken)
	{
		var command = new DeleteStudentCommand(id);
		var result = await _mediator.Send(command, cancellationToken);

		return Content(result);
	}

	#endregion
}
