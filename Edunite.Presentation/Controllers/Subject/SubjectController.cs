using Edunite.Application.Features.Subject;

namespace Edunite.Presentation.Controllers.Subject
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly Mediator mediator;
        public SubjectController(Mediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("GetSubjectByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetSubjectIdByCategory([FromBody]Guid categoryId, CancellationToken ct)
        {
            var query = new GetSubjectsByCategoryQuery(categoryId);
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
