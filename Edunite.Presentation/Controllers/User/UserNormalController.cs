namespace Edunite.Presentation.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNormalController : BaseController
    {
        private readonly IMediator _mediator;

        public UserNormalController(IMediator mediator)
        {
            _mediator = mediator;
        }


       [HttpGet]
       public async Task<IActionResult> GetUserListAsync(int pageNo, int pageSize, CancellationToken cs)
       {
           var query = new GetStudentListQuery(pageNo, pageSize);
           var result = await _mediator.Send(query, cs);

           return Content(result);
       }


	//[HttpGet("GetUserById")]
       public async Task<IActionResult> GetUserByIdAsync(Guid userId, CancellationToken cs)
       {
           var query = new GetStudentByIdQuery(userId);
           var result = await _mediator.Send(query, cs);

           return Content(result);
       }
    }

}
