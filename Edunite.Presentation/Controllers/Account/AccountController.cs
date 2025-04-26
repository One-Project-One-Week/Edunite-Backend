using Edunite.Application.Features.Account.Deactive;
using Edunite.Application.Features.Account.ForgetPassword;
using Edunite.Application.Features.Account.Login;
using Edunite.Application.Features.Account.RefreshToken;
using Edunite.Application.Features.Account.Register;
using Edunite.Application.Features.Account.ResetPassword;
using Edunite.Application.Features.Account.SeedAllRole;
using Edunite.Application.Features.Account.SeedRoleToAdmin;
using Edunite.Application.Features.Account.SeedRoleToTeacher;
using Edunite.DTO.Features.Account.Requests;

namespace Edunite.Presentation.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediatR;
        public AccountController(IMediator mediatoR)
        {
            _mediatR = mediatoR;
        }
        [HttpPost("Seed_Role")]
        public async Task<IActionResult> SeedRoles()
        {
            await _mediatR.Send(new SeedRolesCommand());
            return Ok();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto rq, CancellationToken cancellationToken)
        {
            var command = new UserRegisterCommand(rq);
            await _mediatR.Send(command, cancellationToken);
            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(loginRequest);
            await _mediatR.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email, CancellationToken cancellationToken)
        {
            var comand = new ForgetPasswordCommand(email);
            await _mediatR.Send(comand, cancellationToken);
            return Ok();
        }

        [HttpPatch("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request, CancellationToken cancellationToken)
        {
            var command = new ResetPasswordCommand(request);
            await _mediatR.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateDto request, CancellationToken cancellationToken)
        {
            var commadn = new DeactivateCommand(request);
            await _mediatR.Send(commadn, cancellationToken);
            return Ok();
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] string refresh, CancellationToken cancellationToken)
        {
            var refreshTokenToken = HttpContext.Request.Cookies["refresh_token"];
            var commadn = new RefreshTokenCommand(refreshTokenToken);
            await _mediatR.Send(commadn, cancellationToken);
            return Ok();
        }

        [HttpPost("SeeRoleToAdmin")]
        public async Task<IActionResult> SeeRoleToAdminAsync([FromBody] string email, CancellationToken cancellationToken)
        {
            var command = new SeedRoleToAdminCommand(email);
            await _mediatR.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("SeeRoleToTeacher")]
        public async Task<IActionResult> SeeRoleToSellerAsync([FromBody] string email, CancellationToken cancellationToken)
        {
            var command = new SeedRoleToTeacherCommand(email);
            await _mediatR.Send(command, cancellationToken);
            return Ok();
        }
    }
}
