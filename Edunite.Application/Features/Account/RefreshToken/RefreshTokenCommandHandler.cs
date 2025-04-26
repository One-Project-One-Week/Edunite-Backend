using Edunite.Domain.Features.Account;
using Edunite.DTO.Features.Account.Response;

namespace Edunite.Application.Features.Account.RefreshToken
{
	public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponseDto>
	{
		private readonly IAccount _accountService;
		public RefreshTokenCommandHandler(IAccount accountService)
		{
			_accountService = accountService;
		}
		public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var accesstoken = await _accountService.RefreshTokenAsync(request.RefreshToken);
			return accesstoken;
		}
	}
}
