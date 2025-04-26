using Edunite.Application.Features.Teacher.CreateCourseByTeacher;
using Edunite.Application.Features.Teacher.GetTeacherDetailsById;
using Edunite.DTO.Features.Course.Requests;

namespace Edunite.Presentation.Controllers.Teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : BaseController
    {
        private readonly IMediator _mediator;
        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("TeacherCreateCourse")]
        public async Task<IActionResult> TeacherCreateCourse([FromBody] TeacherCourseCreatingDTO courseDto, CancellationToken cancellationToken)
        {
            var command = new CreateCourseByTeacherCommand(courseDto);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpGet("GetTeacherDetailById/{userId}")]
        public async Task<IActionResult> GetTeacherDetailByUserIdAsync([FromBody]Guid userId, CancellationToken cancel)
        {
            var command = new GetTeacherDetailsByIdQuery(userId);
            var result = await _mediator.Send(command, cancel);
            return Ok(result);
        }
    }
}
