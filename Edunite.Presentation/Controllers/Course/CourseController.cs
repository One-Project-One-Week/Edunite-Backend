using Edunite.Application.Features.Course.CreateCourse;
using Edunite.Application.Features.Course.GetAllCourse;
using Edunite.Application.Features.Course.UpdateCourse;
using Edunite.DTO.Features.Course.Requests;
using Edunite.DTO.Features.Course.responses;
using Microsoft.AspNetCore.Authorization;

namespace Edunite.Presentation.Controllers.Course
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : BaseController
    {
        private readonly IMediator _mediator;
        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses(int pageNo, int pageSize, CancellationToken cancellationToken)
        {
            var query = new GetAllCourseQuery(pageNo,pageSize);
            var result = await _mediator.Send(query, cancellationToken);

            return Content(result);
        }
        [HttpGet("GetCourseById/{courseid}")]
        public async Task<IActionResult> GetCourseById([FromBody]Guid courseid, CancellationToken cancellationToken)
        {
            var query = new GetStudentByIdQuery(courseid);
            var result = await _mediator.Send(query, cancellationToken);
            return Content(result);
        }
        [HttpPatch("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse([FromBody] GetAllCourseDto updateCourseDto, CancellationToken cancellationToken)
        {
            var command = new UpdateCourseCommand(updateCourseDto);
           await _mediator.Send(command, cancellationToken);
            return Ok("Updated Successfully");
        }
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto courseDto, CancellationToken cancellationToken)
        {
            var command = new CreateCourseByAdminCommand(courseDto);
            var result = await _mediator.Send(command, cancellationToken);
            return Content(result);
        }
    }
}
